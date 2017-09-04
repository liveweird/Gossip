using System.Collections.Generic;
using Autofac;
using Autofac.Features.Variance;
using AutoMapper;
using Gossip.Application.Services.Chat;
using Gossip.Domain.Events.Chat;
using Gossip.Domain.External.GraphDataDump;
using Gossip.Domain.Handlers.Chat;
using Gossip.Domain.Models.Chat;
using Gossip.Domain.Repositories.Chat;
using MediatR;
using Moq;
using Xunit;

namespace Gossip.Application.Tests.Chat
{
    public class ChatServiceTest
    {
        private readonly IMapper _mapper;
        private readonly IContainer _container;
        private readonly Mock<INotificationHandler<NewChannelSubmittedEvent>> _eventHandlerMock;

        public ChatServiceTest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfiles("Gossip.Web"));
            _mapper = new Mapper(config);
            (_container, _eventHandlerMock) = CreateContainer();
        }

        private static (IContainer, Mock<INotificationHandler<NewChannelSubmittedEvent>>) CreateContainer()
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
                    return t => (IEnumerable<object>) c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
                })
                .InstancePerLifetimeScope();

            var eventHandlerMock = new Mock<INotificationHandler<NewChannelSubmittedEvent>>();
            eventHandlerMock.Setup(handler => handler.Handle(It.IsAny<NewChannelSubmittedEvent>())).Verifiable();

            builder.RegisterInstance(new Mock<IChannelRepository>().Object).As<IChannelRepository>();
            builder.RegisterInstance(new Mock<IBlobStorage>().Object).As<IBlobStorage>();

            builder.RegisterInstance(eventHandlerMock.Object).As<INotificationHandler<NewChannelSubmittedEvent>>();
            
            return (builder.Build(), eventHandlerMock);
        }

        [Fact]
        public void AddChannel()
        {
            // Arrange
            using (var scope = _container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                var channelRepoMock = scope.Resolve<IChannelRepository>();

                // Act
                var chatService = new ChatService(_mapper, mediator, channelRepoMock);
                chatService.AddChannel(new Models.Chat.Channel
                {
                    Name = "abc",
                    Description = "def"
                });

                // Assert
                _eventHandlerMock.Verify(handler => handler.Handle(It.IsAny<NewChannelSubmittedEvent>()), Times.Exactly(1));
            }
        }
    }
}
