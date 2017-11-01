using System.Collections.Generic;
using System.Linq;
using Gossip.Domain.Events.Chat;

namespace Gossip.Domain.Models.Chat
{
    public class UserInChannel : Entity
    {
        public int UserId { get; internal set; }
    }

    public class Channel : AggregateRoot
    {       
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public List<Message> Messages { get; internal set; }
        public List<UserInChannel> Users { get; internal set; }

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

        public bool IsInChannel(int userId)
        {
            return Users.Any(u => u.UserId == userId);
        }

        public void MakeUserJoin(int userId)
        {
            Users.Add(new UserInChannel
            {
                UserId = userId
            });
        }

        public void MakeUserLeave(int userId)
        {
            Users.RemoveAll(u => u.UserId == userId);
        }
    }
}