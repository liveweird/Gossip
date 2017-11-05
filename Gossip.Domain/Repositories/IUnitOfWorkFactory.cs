using System.Threading.Tasks;

namespace Gossip.Domain.Repositories
{
    public interface IUnitOfWorkFactory
    {
        Task<IUnitOfWork> CreateAsync();
        IUnitOfWork Create();
    }
}