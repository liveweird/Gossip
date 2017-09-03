using System.Collections.Generic;
using Channel = Gossip.Application.Models.Channel;

namespace Gossip.Application
{
    public interface IChatService
    {
        void AddChannel(Channel channel);
        IEnumerable<Channel> GetAllChannels();
    }
}