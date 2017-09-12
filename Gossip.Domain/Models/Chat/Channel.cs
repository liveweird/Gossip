using System.Collections.Generic;
using Gossip.Domain.Events.Chat;

namespace Gossip.Domain.Models.Chat
{
    public class Channel : AggregateRoot
    {       
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public List<Message> Messages { get; internal set; }

        /// <summary>
        /// Internal contstructor is required by ORM
        /// </summary>
        internal Channel()
        {            
        }

        /// <summary>
        /// Public constructor players a role of a factory method, it should expose all the parameters that are important to build a semantically proper entity
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public Channel(string name, string description)
        {
            Name = name;
            Description = description;
            Messages = new List<Message>();
        }

        public bool IsEmpty()
        {
            return Messages.Count == 0;
        }

        public void AddMessage(string content, Message parentMessage = null)
        {
            Messages.Add(new Message(content, parentMessage));

            RaiseDomainEvent(new NewMessageCreatedEvent
            {
                Content = content
            });
        }

        public void RemoveMessage(Message message)
        {
            Messages.Remove(message);
        }
    }
}