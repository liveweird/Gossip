using System.Collections.Generic;
using System.Threading.Tasks;
using Gossip.Application.Models.Chat;
using Channel = Gossip.Application.Models.Chat.Channel;

namespace Gossip.Application.Contracts.Chat
{
    public interface IChatService
    {
        Task<bool> AddChannel(Channel channel);
        Task<bool> AddMessage(Message message);
        Task<IEnumerable<Channel>> GetAllChannels();
        Task<IEnumerable<Message>> GetAllMessagesInChannel(int channelId);
    }
}