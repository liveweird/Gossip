using System.Linq;
using System.Threading.Tasks;
using Gossip.Domain.Models;
using MediatR;

namespace Gossip.SQLite
{
    static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, GossipContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.DomainEvents.Clear());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}