using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gossip.Domain.Models;
using MediatR;

namespace Gossip.Domain
{
    public static class DomainMediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, IEnumerable<Entity> entities)
        {
            var domainEvents = entities
                .SelectMany(x => x.DomainEvents)
                .ToList();

            entities.ToList()
                .ForEach(entity => entity.DomainEvents.Clear());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}