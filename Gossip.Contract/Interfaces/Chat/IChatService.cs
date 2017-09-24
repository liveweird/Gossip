using System.Collections.Generic;
using System.Threading.Tasks;
using Gossip.Contract.DTO.Chat;
using LanguageExt;
using Channel = Gossip.Contract.DTO.Chat.Channel;

namespace Gossip.Contract.Interfaces.Chat
{
    public interface IChatService
    {
        Task<Unit> AddChannel(Channel channel);
        Task AddMessage(Message message);
        Task<Lst<Channel>> GetAllChannels();
        Task<IEnumerable<Message>> GetAllMessagesInChannel(int channelId);
    }
}