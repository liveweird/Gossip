using System.Collections.Generic;
using System.Threading.Tasks;
using Gossip.Contract.DTO.Chat;
using Channel = Gossip.Contract.DTO.Chat.Channel;

namespace Gossip.Contract.Chat
{
    public interface IChatService
    {
        Task<bool> AddChannel(Channel channel);
        Task<bool> AddMessage(Message message);
        Task<IEnumerable<Channel>> GetAllChannels();
        Task<IEnumerable<Message>> GetAllMessagesInChannel(int channelId);
    }
}