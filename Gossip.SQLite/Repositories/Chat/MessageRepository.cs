using Gossip.Domain.Models.Chat;
using Gossip.Domain.Repositories.Chat;

namespace Gossip.SQLite.Repositories.Chat
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(GossipContext context) : base(context)
        {
        }
    }
}