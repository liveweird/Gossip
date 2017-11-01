using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gossip.Domain.Repositories.User
{
    public interface IUserRepository : IRepository<Models.User.User>
    {
        Models.User.User InsertChannel(Models.User.User channel);
        Models.User.User UpdateChannel(Models.User.User channel);
        Task<IEnumerable<Models.User.User>> GetAllUsers();
        Task<IEnumerable<Models.User.User>> GetUsers(IEnumerable<int> ids);
    }
}