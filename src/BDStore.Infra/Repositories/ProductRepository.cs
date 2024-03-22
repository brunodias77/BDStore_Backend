using BDStore.Domain.Abstraction;
using BDStore.Domain.Products;
using BDStore.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace BDStore.Infra.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public IUnitOfWork UnitOfWork => _context;


    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<Product> GetById(Guid id)
    {
        return await _context.Products.FindAsync(id);
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}