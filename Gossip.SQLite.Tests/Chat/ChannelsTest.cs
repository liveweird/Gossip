using System.Collections.Generic;
using Autofac;
using Autofac.Features.Variance;
using Gossip.Domain.Models.Chat;
using Gossip.SQLite.Repositories.Chat;
using MediatR;
using Xunit;

namespace Gossip.SQLite.Tests.Chat
{
    public class ChannelsTest
    {
        private readonly IContainer _container;

        public ChannelsTest()
        {
            _container = CreateContainer();
        }

        private static IContainer CreateContainer()
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

            return builder.Build();
        }

        [Fact]
        public async void AddAndVerifyChannels()
        {
            // Arrange
            using (var scope = _container.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                using (var ctx = new GossipContext())
                using (var uow = await new UnitOfWorkFactory<GossipContext>(ctx, mediator).CreateAsync())
                {
                    var channelRepo = new ChannelRepository(ctx);
                    ctx.Channels.RemoveRange(ctx.Channels);

                    // Act
                    channelRepo.InsertChannel(new Channel(name: "abc", description: "def"));
                    await uow.CommitChangesAsync();

                    var channels = await channelRepo.GetAllChannels();

                    // Assert
                    Assert.Collection(channels, p =>
                    {
                        Assert.Equal("abc", p.Name);
                        Assert.Equal("def", p.Description);
                    });
                }
            }
        }
    }
}
