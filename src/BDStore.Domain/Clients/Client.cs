using BDStore.Domain.Abstraction;
using BDStore.Domain.DomainObjects;

namespace BDStore.Domain.Clients;

public class Client : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Cpf Cpf { get; private set; }
    public bool Deleted { get; private set; }
    public Address Address { get; private set; }

    //Ef Relation 
    protected Client()
    {
    }

    public Client(Guid id, string name, string email, string cpf)
    {
        Id = id; // Id vindo do Identity
        Name = name;
        Email = new Email(email);
        Cpf = new Cpf(cpf);
        Deleted = false;
    }

    public void ChangeEmail(string email) => Email = new Email(email);

    public void AssignAddress(Address address)
    {
        Address = address;
    }
}