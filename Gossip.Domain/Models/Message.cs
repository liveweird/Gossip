namespace Gossip.Domain.Models
{
    public class Message : IEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int ChannelId { get; set; }
        public Channel Channel { get; set; }
    }
}