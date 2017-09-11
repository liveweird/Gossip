namespace Gossip.Domain.Models.Chat
{
    public class Message : Entity
    {
        public string Content { get; set; }
        public int Likes { get; set; }
        public int? ParentId { get; }

        public Message(string content, int? parentMessageId = null)
        {
            Content = content;
            Likes = 0;
            ParentId = parentMessageId;
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