using BDStore.Domain.Abstraction;
using BDStore.Domain.Users.ValueObjects;

namespace BDStore.Domain.Users.Events
{
    public class UserCreatedDomainEvent : DomainEvent
    {
        public UserId UserId { get; }

        public UserCreatedDomainEvent(UserId userId)
        {
            UserId = userId;
        }
    }
}