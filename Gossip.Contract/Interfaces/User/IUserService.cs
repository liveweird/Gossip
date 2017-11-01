using System.Threading.Tasks;
using LanguageExt;

namespace Gossip.Contract.Interfaces.User
{
    public interface IUserService
    {
        Task<Unit> AddUser(DTO.User.User user);
        Task<Lst<DTO.User.User>> GetAllUsers();
        Task<Lst<DTO.User.User>> GetUsers(Lst<int> ids);
    }
}