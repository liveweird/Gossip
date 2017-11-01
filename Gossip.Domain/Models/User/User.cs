namespace Gossip.Domain.Models.User
{
    public class User : AggregateRoot
    {
        public string Name { get; internal set; }
        public string Description { get; internal set; }
    }
}