using Gossip.Domain.Models.Chat;
using Gossip.Domain.Repositories.Chat;

namespace Gossip.SQLite.Repositories.Chat
{
    public class ChannelRepository : Repository<Channel>, IChannelRepository
    {
        public ChannelRepository(GossipContext context) : base(context)
        {
        }
    }
}