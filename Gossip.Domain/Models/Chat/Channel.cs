using System;
using System.Collections.Generic;
using System.Linq;
using Gossip.Domain.Events.Chat;

namespace Gossip.Domain.Models.Chat
{
    public class Channel : Entity, IAggregateRoot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Message> Messages { get; set; }

        public bool IsEmpty()
        {
            return Messages.Count == 0;
        }

        public void AddMessage(int? parentId, string content)
        {
            var parent = parentId != null ? Messages.Single(m => m.Id == parentId) : null;

            Messages.Add(new Message
            {
                Content = content,
                ChannelId = Id,
                Channel = this,
                ParentId = parentId,
                Parent = parent
            });

            RaiseDomainEvent(new NewMessageCreatedEvent
            {
                Content = content
            });
        }

        public void RemoveMessage(int messageId)
        {
            var toBeRemoved = Messages.Single(m => m.Id == messageId);

            if (Messages.Any(m => m.ParentId == messageId))
            {
                throw new InvalidOperationException("Message has children");
            }

            Messages.Remove(toBeRemoved);
        }
    }
}