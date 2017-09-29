using Autofac;
using Gossip.Application.Services.Chat;
using Gossip.Contract.Interfaces.Chat;
using Gossip.Domain.External.BlobStorage;
using Gossip.Domain.Repositories;
using Gossip.Domain.Repositories.Chat;
using Gossip.DynamoDb.BlobStorage;
using Gossip.SQLite;
using Gossip.SQLite.Repositories.Chat;

namespace Gossip.Web
{
    public class CompositionRoot : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.SetUpMediator()
                .SetUpAutoMapper();

            // Entity Framework

            builder.RegisterType<GossipContext>().As<GossipContext>().InstancePerLifetimeScope();

            // Custom Deps

            builder.RegisterType<ChatService>().As<IChatService>().InstancePerLifetimeScope();
            builder.RegisterType<ChannelRepository>().As<IChannelRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DynamoDbBlobStorage>().As<IBlobStorage>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWorkFactory<GossipContext>>().As<IUnitOfWorkFactory>().InstancePerLifetimeScope();
            
            base.Load(builder);
        }
    }
}
