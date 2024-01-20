using BDStore.Domain.Abstraction;
using BDStore.Domain.Users.Events;
using BDStore.Domain.Users.ValueObjects;

namespace BDStore.Domain.Users
{
    public class User : Entity
    {
        public UserId Id { get; private set; }
        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public Email Email { get; private set; }
        public string Password { get; private set; }

        public User() { }
        public User(FirstName firstName, LastName lastName, Email email, string password)
        {
            Id = new UserId(Guid.NewGuid());
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            NotifyDomainEvent(new UserCreatedDomainEvent(this.Id));
        }

        public static User CreateRegistered(FirstName firstName, LastName lastName, Email email, string password)
        {
            return new User(firstName, lastName, email, password);
        }
    }
}