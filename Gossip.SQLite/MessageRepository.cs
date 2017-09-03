using Gossip.Domain;
using Gossip.Domain.Models;

namespace Gossip.SQLite
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(GossipContext context) : base(context)
        {
        }
    }
}