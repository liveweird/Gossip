using System.Collections.Generic;
using MediatR;

namespace Gossip.Domain.Models
{
    public class Entity : IEntity
    {
        public int Id { get; set; }

        private List<INotification> _domainEvents;
        public List<INotification> DomainEvents => _domainEvents;

        public void RaiseDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }
    }
}
