using System.Linq;
using System.Threading.Tasks;
using Gossip.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gossip.SQLite
{
    public static class SQLiteMediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext ctx)
        {
            var entities = ctx.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .Select(p => p.Entity);

            await Domain.DomainMediatorExtension.DispatchDomainEventsAsync(mediator, entities);
        }
    }
}