using BDStore.Domain.Abstraction;

namespace BDStore.Domain.Clients;

public class Address : Entity
{
    public string StreetAddress { get; private set; }
    public string Number { get; private set; }
    public string Complement { get; private set; }
    public string Neighborhood { get; private set; }
    public string ZipCode { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public Guid ClientId { get; private set; }

    //EF Relation
    public Client Client { get; private set; }

    public Address(string streetAddress, string number, string complement, string neighborhood, string zipCode,
        string city, string state, Guid clientId)
    {
        StreetAddress = streetAddress;
        Number = number;
        Complement = complement;
        Neighborhood = neighborhood;
        ZipCode = zipCode;
        City = city;
        State = state;
        ClientId = clientId;
    }
}