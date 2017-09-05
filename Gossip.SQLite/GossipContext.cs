using System;
using System.Threading;
using System.Threading.Tasks;
using Gossip.Domain.Models.Chat;
using Gossip.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gossip.SQLite
{
    public class GossipContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public DbSet<Channel> Channels { get; set; }
        public DbSet<Message> Messages { get; set; }

        public GossipContext(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public GossipContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=gossip.db");
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);
            var result = await base.SaveChangesAsync();

            return true;
        }
    }
}
