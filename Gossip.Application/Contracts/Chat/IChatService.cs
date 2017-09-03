using System.Collections.Generic;
using Channel = Gossip.Application.Models.Chat.Channel;

namespace Gossip.Application.Contracts.Chat
{
    public interface IChatService
    {
        void AddChannel(Channel channel);
        IEnumerable<Channel> GetAllChannels();
    }
}