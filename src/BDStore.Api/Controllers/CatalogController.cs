using BDStore.Domain.Products;

namespace BDStore.Api.Controllers;

public class CatalogController
{
    private readonly IProductRepository _productRepository;

    public CatalogController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [AllowAnonymous]
    [HttpGet("catalog/products")]
    public async Task<IEnumerable<Product>> Index()
    {
        Console.WriteLine("Aqui no Controller do catalog no metodo index()");
        return await _productRepository.GetAll();
    }

    [ClaimsAuthorize("Catalogo", "Ler")]
    [HttpGet("catalog/product/{id}")]
    public async Task<Product> ProductDetail(Guid id)
    {
        Console.WriteLine($"Aqui no CatalogController com o id: {id}");
        return await _productRepository.GetById(id);
    }
}