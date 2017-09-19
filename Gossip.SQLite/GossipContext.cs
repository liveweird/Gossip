using Gossip.Domain.Models.Chat;
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

    public class GossipContext : DbContext
    {
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Message> Messages { get; set; }

        internal GossipContext(DbContextOptions options) : base(options)
        {
            // two important comments:
            // 1. this contstructor is left 'internal' for a reason - it should be use of for the purpose of creating migrations
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=gossip.db");
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
