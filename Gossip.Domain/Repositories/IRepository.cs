using System.Threading.Tasks;
using Gossip.Domain.Models;

namespace Gossip.Domain.Repositories
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task<T> GetAsync(int id);
    }
}