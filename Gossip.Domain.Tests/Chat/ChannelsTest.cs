using System.Collections.Generic;
using Autofac;
using Autofac.Features.Variance;
using Gossip.Domain.Events.Chat;
using Gossip.Domain.External.BlobStorage;
using Gossip.Domain.Models.Chat;
using Gossip.Domain.Repositories.Chat;
using MediatR;
using Moq;
using Xunit;

namespace Gossip.Domain.Tests.Chat
{
    public class ChannelsTest
    {
        private readonly IContainer _container;
        private readonly Mock<INotificationHandler<NewMessageCreatedEvent>> _eventHandlerMock;

        public ChannelsTest()
        {
            (_container, _eventHandlerMock) = CreateContainer();
        }

        private static (IContainer, Mock<INotificationHandler<NewMessageCreatedEvent>>) CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new ContravariantRegistrationSource());

            builder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder
                .Register<SingleInstanceFactory>(ctx =>
                {
                    var c = ctx.Resolve<IComponentContext>();
                    return t =>
                    {
                        object o;
                        return c.TryResolve(t, out o) ? o : null;
                    };
                })
                .InstancePerLifetimeScope();

            builder
                .Register<MultiInstanceFactory>(ctx =>
                {
                    var c = ctx.Resolve<IComponentContext>();
                    return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
                })
                .InstancePerLifetimeScope();

            var eventHandlerMock = new Mock<INotificationHandler<NewMessageCreatedEvent>>();
            eventHandlerMock.Setup(handler => handler.Handle(It.IsAny<NewMessageCreatedEvent>())).Verifiable();

            builder.RegisterInstance(new Mock<IChannelRepository>().Object).As<IChannelRepository>();
            builder.RegisterInstance(new Mock<IBlobStorage>().Object).As<IBlobStorage>();

            builder.RegisterInstance(eventHandlerMock.Object).As<INotificationHandler<NewMessageCreatedEvent>>();

            return (builder.Build(), eventHandlerMock);
        }

        [Fact]
        public async void AddMessageSideEffect()
        {
            // Arrange
            using (var scope = _container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                // Act
                var channel = new Channel(name: "abc", description: "def");

                channel.AddMessage("ghi");
                await mediator.DispatchDomainEventsAsync(new List<Channel> { channel });

                // Assert
                _eventHandlerMock.Verify(handler => handler.Handle(It.IsAny<NewMessageCreatedEvent>()), Times.Exactly(1));
            }
        }
    }
}
