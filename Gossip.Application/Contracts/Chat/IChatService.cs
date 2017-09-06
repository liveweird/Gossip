using System.Collections.Generic;
using System.Threading.Tasks;
using Channel = Gossip.Application.Models.Chat.Channel;

namespace Gossip.Application.Contracts.Chat
{
    public interface IChatService
    {
        Task<bool> AddChannel(Channel channel);
        Task<bool> AddMessage(int channelId, int? parentId, string content);
        IEnumerable<Channel> GetAllChannels();
    }
}