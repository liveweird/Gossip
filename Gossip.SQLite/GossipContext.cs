using System;
using System.Threading;
using System.Threading.Tasks;
using Gossip.Domain.Models.Chat;
using Gossip.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Gossip.SQLite
{
    public class GossipContextFactory : IDesignTimeDbContextFactory<GossipContext>
    {
        public GossipContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<GossipContext>();
            builder.UseSqlite("Data Source=gossip.db");
            return new GossipContext(builder.Options);
        }
    }

    public class GossipContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public DbSet<Channel> Channels { get; set; }
        public DbSet<Message> Messages { get; set; }

        internal GossipContext(DbContextOptions options) : base(options)
        {
            // two important comments:
            // 1. this contstructor is left 'internal' for a reason - it should be use of for the purpose of creating migrations
            // 2. in this particular usage, _mediator is not used (domain events are not raised), so it can be left null
            _mediator = null;
        }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Channel>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Message>()
                .HasKey(c => c.Id);
        }
    }
}
