using System.Collections.Generic;
using System.Threading.Tasks;
using Gossip.Domain.Models.Chat;

namespace Gossip.Domain.Repositories.Chat
{
    public interface IChannelRepository : IRepository<Channel>
    {
        Channel InsertChannel(Channel channel);
        void UpdateChannel(Channel channel);
        Task<IEnumerable<Channel>> GetAllChannels();
    }
}
