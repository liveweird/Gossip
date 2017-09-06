namespace Gossip.Web.ViewModels.Dashboard
{
    public class Message
    {
        public int? Id { get; set; }
        public int ChannelId { get; set; }
        public int? ParentId { get; set; }
        public string Content { get; set; }
    }
}