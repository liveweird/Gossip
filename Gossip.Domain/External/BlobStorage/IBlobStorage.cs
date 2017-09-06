using Gossip.Domain.Models.Chat;

namespace Gossip.Domain.External.BlobStorage
{
    public interface IBlobStorage
    {
        void DoSomething(Message message);
        void DoSomething(Channel channel);
    }
}
