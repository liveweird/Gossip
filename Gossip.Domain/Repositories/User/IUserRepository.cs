using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gossip.Domain.Repositories.User
{
    public interface IUserRepository : IRepository<Models.User.User>
    {
        Models.User.User InsertUser(Models.User.User user);
        void UpdateUser(Models.User.User user);
        Task<IEnumerable<Models.User.User>> GetAllUsers();
        Task<IEnumerable<Models.User.User>> GetUsers(IEnumerable<int> ids);
    }
}