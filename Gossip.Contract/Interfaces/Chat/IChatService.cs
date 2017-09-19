using System.Collections.Generic;
using System.Threading.Tasks;
using Gossip.Contract.DTO.Chat;
using Channel = Gossip.Contract.DTO.Chat.Channel;

namespace Gossip.Contract.Interfaces.Chat
{
    public interface IChatService
    {
        Task AddChannel(Channel channel);
        Task AddMessage(Message message);
        Task<IEnumerable<Channel>> GetAllChannels();
        Task<IEnumerable<Message>> GetAllMessagesInChannel(int channelId);
    }
}