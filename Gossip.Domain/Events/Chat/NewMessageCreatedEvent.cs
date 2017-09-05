using MediatR;

namespace Gossip.Domain.Events.Chat
{
    public class NewMessageCreatedEvent : INotification
    {
        public string Content { get; set; }
    }
}