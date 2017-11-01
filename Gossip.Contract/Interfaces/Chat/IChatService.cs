using System.Threading.Tasks;
using Gossip.Contract.DTO.Chat;
using LanguageExt;
using Channel = Gossip.Contract.DTO.Chat.Channel;

namespace Gossip.Contract.Interfaces.Chat
{
    public interface IChatService
    {
        Task<Unit> AddChannel(Channel channel);
        Task<Unit> AddMessage(Message message);
        Task<Lst<Channel>> GetAllChannels();
        Task<Lst<Message>> GetAllMessagesInChannel(int channelId);
        Task<Unit> MakeUserJoin(int channelId, int userId);
        Task<Unit> MakeUserLeave(int channelId, int userId);
        Task<Lst<int>> GetIdsOfUsersInChannel(int channelId);
    }
}