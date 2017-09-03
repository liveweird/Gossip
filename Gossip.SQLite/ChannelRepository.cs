using Gossip.Domain;
using Gossip.Domain.Models;

namespace Gossip.SQLite
{
    public class ChannelRepository : Repository<Channel>, IChannelRepository
    {
        public ChannelRepository(GossipContext context) : base(context)
        {
        }
    }
}