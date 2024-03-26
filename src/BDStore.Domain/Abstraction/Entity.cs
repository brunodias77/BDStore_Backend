using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDStore.Domain.Abstraction
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        private List<IDomainEvent> _domainEvents;
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        private List<Event> _notifications;
        public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public void AddEvent(Event @event)
        {
            _notifications ??= new List<Event>();
            _notifications.Add(@event);
        }

        protected void NotifyDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            this._domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            this._domainEvents?.Clear();
        }
        
        public void ClearEvents()
        {
            _notifications?.Clear();
        }
    }
}