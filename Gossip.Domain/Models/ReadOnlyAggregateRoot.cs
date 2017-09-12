namespace Gossip.Domain.Models
{
    public class ReadOnlyAggregateRoot : IReadOnlyAggregateRoot
    {
        public int Id { get; }
    }
}