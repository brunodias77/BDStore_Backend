using BDStore.Domain.Abstraction;
using BDStore.Domain.Clients;
using BDStore.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace BDStore.Infra.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ApplicationDbContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public ClientRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public void Add(Client client)
    {
        _context.Clients.Add(client);
    }

    public void AddAddress(Address address)
    {
    }

    public Task<Address> GetAddressById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Client>> GetAll()
    {
        return await _context.Clients.AsNoTracking().ToListAsync();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }

    public Task<Client> GetByCpf(string cpf)
    {
        return _context.Clients.FirstOrDefaultAsync(customer => customer.Cpf.Number == cpf);
    }
}