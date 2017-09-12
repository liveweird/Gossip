namespace Gossip.Domain.Models.Chat
{
    public class Message : Entity
    {
        public string Content { get; internal set; }
        public int Likes { get; internal set; }
        public Message Parent { get; internal set; }

        /// <summary>
        /// Internal constructor is required by the ORM
        /// </summary>
        internal Message()
        {            
        }

        /// <summary>
        /// Public constructor plays the role of a factory method, it should expose all the parameters that are required to build a semantically proper entity
        /// </summary>
        /// <param name="content"></param>
        /// <param name="parentMessageId"></param>
        public Message(string content, Message parent = null)
        {
            Content = content;
            Likes = 0;
            Parent = parent;
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