using System.Collections.Generic;

namespace Gossip.Domain.Models.Chat
{
    public class Message : Entity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }

        public int ChannelId { get; set; }
        public Channel Channel { get; set; }

        public int? ParentId { get; set; }
        public Message Parent { get; set; }

        public Message()
        {
            Likes = 0;
        }

        public void Like()
        {
            Likes++;
        }

        public void Unlike()
        {
            if (Likes >= 1)
            {
                Likes--;
            }
        }
    }
}