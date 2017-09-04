using Gossip.Domain.Models.Chat;

namespace Gossip.Domain.External.GraphDataDump
{
    public interface IBlobStorage
    {
        void DoSomething(Message message);
        void DoSomething(Channel channel);
    }
}
