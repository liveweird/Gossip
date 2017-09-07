using System;
using System.Collections.Generic;
using System.Linq;
using Gossip.Domain.Events.Chat;

namespace Gossip.Domain.Models.Chat
{
    public class Channel : AggregateRoot, IAggregateRoot
    {
        private List<Message> _mesasges;
        
        public string Name { get; set; }
        public string Description { get; set; }

        public IReadOnlyCollection<Message> Messages => _mesasges.AsReadOnly();

        public Channel(string name)
        {
            Name = name;
            _mesasges = new List<Message>();
        }

        public bool IsEmpty()
        {
            return Messages.Count == 0;
        }

        public void AddMessage(string content, int? parentMessageId = null)
        {
            _mesasges.Add(new Message(content, parentMessageId));

            RaiseDomainEvent(new NewMessageCreatedEvent
            {
                Content = content
            });
        }

        public void RemoveMessage(Message message)
        {
            _mesasges.Remove(message);
        }
    }
}