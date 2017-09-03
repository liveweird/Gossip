using Gossip.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gossip.SQLite
{
    public class GossipContext : DbContext
    {
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=gossip.db");
        }
    }
}
