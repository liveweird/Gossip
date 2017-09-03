using System.Collections.Generic;
using Gossip.Domain.Models;

namespace Gossip.Application
{
    public interface IChatService
    {
        void AddChannel(string name, string description);
        IEnumerable<Channel> GetAllChannels();
    }
}