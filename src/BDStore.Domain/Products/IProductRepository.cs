using BDStore.Domain.Abstraction;

namespace BDStore.Domain.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(Guid id);
        void Add(Product product);
        void Update(Product product);
    }
}