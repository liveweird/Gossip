using System.Collections.Generic;
using Autofac;
using Autofac.Features.Variance;
using Gossip.Domain.Events.Chat;
using Gossip.Domain.External.GraphDataDump;
using Gossip.Domain.Handlers.Chat;
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
        private readonly Mock<IChannelRepository> _channelRepoMock;

        public ChannelsTest()
        {
            (_container, _channelRepoMock) = CreateContainer();
        }

        private static (IContainer, Mock<IChannelRepository>) CreateContainer()
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

            var channelRepoMock = new Mock<IChannelRepository>();
            channelRepoMock.Setup(repo => repo.Insert(new Channel()))
                .Verifiable();

            builder.RegisterInstance(channelRepoMock.Object).As<IChannelRepository>();
            builder.RegisterInstance(new Mock<IBlobStorage>().Object).As<IBlobStorage>();

            builder.RegisterType<SaveNewChannelToDatabase>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<DumpNewChannelToBlobStorage>().AsImplementedInterfaces().InstancePerDependency();

            return (builder.Build(), channelRepoMock);
        }

        [Fact]
        public async void AddChannel()
        {
            // Arrange
            var mock = new Mock<IChannelRepository>();
            mock.Setup(repo => repo.GetAll()).Returns(new List<Channel>());

            using (var scope = _container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                // Act
                await mediator.Publish(new NewChannelSubmittedEvent
                {
                    Name = "abc",
                    Description = "def"
                });

                // Assert
                _channelRepoMock.Verify(repo => repo.Insert(It.IsAny<Channel>()), Times.Exactly(1));
            }
        }
    }
}
