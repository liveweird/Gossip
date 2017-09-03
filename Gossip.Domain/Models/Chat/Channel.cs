using System.Collections.Generic;

namespace Gossip.Domain.Models.Chat
{
    public class Channel : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Message> Messages { get; set; }
    }
}