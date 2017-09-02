using System.Collections.Generic;

namespace Gossip.SQLite.Models
{
    public class Channel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Message> Messages { get; set; }
    }
}