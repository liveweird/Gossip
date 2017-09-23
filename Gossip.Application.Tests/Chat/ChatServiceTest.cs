using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Features.Variance;
using AutoMapper;
using Gossip.Application.Handlers.Chat;
using Gossip.Application.Services.Chat;
using Gossip.Domain.Events.Chat;
using Gossip.Domain.External.BlobStorage;
using Gossip.Domain.Models.Chat;
using Gossip.Domain.Repositories;
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
        private readonly Mock<IBlobStorage> _blobStorageMock;

        public ChatServiceTest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfiles("Gossip.Web"));
            _mapper = new Mapper(config);
            (_container, _blobStorageMock) = CreateContainer();
        }

        private static (IContainer, Mock<IBlobStorage>) CreateContainer()
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

            var eventHandlerMock = new Mock<INotificationHandler<NewMessageCreatedEvent>>();
            eventHandlerMock.Setup(handler => handler.Handle(It.IsAny<NewMessageCreatedEvent>())).Verifiable();

            var blobStorageMock = new Mock<IBlobStorage>();
            blobStorageMock.Setup(storage => storage.DoSomething(It.IsAny<Message>())).Verifiable();

            builder.RegisterInstance(blobStorageMock.Object).As<IBlobStorage>();
            builder.RegisterType<DumpNewMessageToBlobStorage>().AsImplementedInterfaces();
           
            return (builder.Build(), blobStorageMock);
        }

        [Fact]
        public async void AddChannel()
        {
            // Arrange
            var uowMock = new Mock<IUnitOfWork>();
            var uowFactoryMock = new Mock<IUnitOfWorkFactory>();
            uowMock.Setup(u => u.CommitChangesAsync()).Returns(Task.CompletedTask);
            uowFactoryMock.Setup(f => f.CreateAsync()).ReturnsAsync(uowMock.Object);

            var channelRepoMock = new Mock<IChannelRepository>();
            channelRepoMock.Setup(repo => repo.InsertChannel(It.IsAny<Channel>())).Verifiable();

            // Act
            var chatService = new ChatService(_mapper, channelRepoMock.Object, uowFactoryMock.Object);
            await chatService.AddChannel(new Contract.DTO.Chat.Channel
            {
                Name = "abc",
                Description = "def"
            }).Invoke();

            // Assert
            channelRepoMock.Verify(repo => repo.InsertChannel(It.IsAny<Channel>()));
            uowMock.Verify(uow => uow.CommitChangesAsync());
        }

        [Fact]
        public async void AddMessageSideEffect()
        {
            // Arrange
            var mock = new Mock<IChannelRepository>();
            mock.Setup(repo => repo.GetAllChannels()).Returns(Task.FromResult(new List<Channel>().AsEnumerable()));

            using (var scope = _container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                // Act
                await mediator.Publish(new NewMessageCreatedEvent
                {
                    Content = "abc"
                });

                // Assert
                _blobStorageMock.Verify(storage => storage.DoSomething(It.IsAny<Message>()), Times.Exactly(1));
            }
        }
    }
}
