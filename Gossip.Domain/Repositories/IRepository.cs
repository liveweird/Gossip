using System.Collections.Generic;
using Gossip.Domain.Models;

namespace Gossip.Domain.Repositories
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        IEnumerable<T> GetAll();
        T Get(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}