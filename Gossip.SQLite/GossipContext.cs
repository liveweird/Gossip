using Microsoft.EntityFrameworkCore;

namespace Gossip.SQLite
{
    public class GossipContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=gossip.db");
        }
    }
}
