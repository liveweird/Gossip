using System.Collections.Generic;
using MediatR;

namespace Gossip.Domain.Models
{
    public class AggregateRoot : Entity, IAggregateRoot
    {
        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public AggregateRoot()
        {
            _domainEvents = new List<INotification>();
        }

        public void RaiseDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void ClearEvents()
        {
            _domainEvents.Clear();
        }
    }
}