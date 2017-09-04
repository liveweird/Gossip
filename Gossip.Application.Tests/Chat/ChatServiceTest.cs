using System.Collections.Generic;
using Autofac;
using Autofac.Features.Variance;
using AutoMapper;
using Gossip.Application.Services.Chat;
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
        private readonly Mock<IChannelRepository> _channelRepoMock;

        public ChatServiceTest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfiles("Gossip.Web"));
            _mapper = new Mapper(config);
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
                    return t => (IEnumerable<object>) c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
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
        public void AddChannel()
        {
            // Arrange
            using (var scope = _container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                // Act
                var chatService = new ChatService(_mapper, mediator, _channelRepoMock.Object);
                chatService.AddChannel(new Models.Chat.Channel
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
