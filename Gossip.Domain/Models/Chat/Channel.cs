using System.Collections.Generic;
using Gossip.Domain.Events.Chat;

namespace Gossip.Domain.Models.Chat
{
    public class Channel : AggregateRoot
    {
        private List<Message> _messages;
        
        public string Name { get; set; }
        public string Description { get; set; }

        public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

        public Channel(string name)
        {
            Name = name;
            _messages = new List<Message>();
        }

        public bool IsEmpty()
        {
            return Messages.Count == 0;
        }

        public void AddMessage(string content, int? parentMessageId = null)
        {
            _messages.Add(new Message(content, parentMessageId));

            RaiseDomainEvent(new NewMessageCreatedEvent
            {
                Content = content
            });
        }

        public void RemoveMessage(Message message)
        {
            _messages.Remove(message);
        }
    }
}