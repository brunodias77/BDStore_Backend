using BDStore.Domain.Abstraction;

namespace BDStore.Domain.Clients;

public interface IClientRepository : IRepository<Client>
{
    void Add(Client client);
    Task<IEnumerable<Client>> GetAll();
    Task<Client> GetByCpf(string cpf);
    void AddAddress(Address address);
    Task<Address> GetAddressById(Guid id);
}