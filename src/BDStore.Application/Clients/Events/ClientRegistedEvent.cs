using BDStore.Domain.Abstraction;

namespace BDStore.Application.Clients.Events;

public class ClientRegistedEvent : Event
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }

    public ClientRegistedEvent(Guid id, string name, string email, string cpf)
    {
        AggregateId = id;
        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
    }
}