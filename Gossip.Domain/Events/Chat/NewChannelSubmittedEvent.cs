using MediatR;

namespace Gossip.Domain.Events.Chat
{
    public class NewChannelSubmittedEvent : INotification
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}