using System.Threading.Tasks;
using Gossip.Domain.Models;

namespace Gossip.Domain.Repositories
{
    public interface IReadOnlyRepository<T> where T : IReadOnlyAggregateRoot
    {
        Task<T> GetAsync(int id);
    }
}