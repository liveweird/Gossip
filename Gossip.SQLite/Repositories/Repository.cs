using System.Threading.Tasks;
using Gossip.Domain.Models;
using Gossip.Domain.Repositories;

namespace Gossip.SQLite.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity, IAggregateRoot
    {
        protected readonly GossipContext Context;

        protected Repository(GossipContext context)
        {
            Context = context;
        }

        public IUnitOfWork UnitOfWork => Context;

        public abstract Task<T> GetAsync(int id);
    }
}