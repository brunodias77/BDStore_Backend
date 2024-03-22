using BDStore.Api.Authorization;
using BDStore.Domain.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BDStore.Api.Controllers;

[Authorize]
[Route("catalog")]
public class CatalogController : MainController
{
    private readonly IProductRepository _productRepository;

    public CatalogController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [AllowAnonymous]
    [HttpGet("products")]
    public async Task<IEnumerable<Product>> Index()
    {
        Console.WriteLine("Aqui no Controller do catalog no metodo index()");
        return await _productRepository.GetAll();
    }

    [ClaimsAuthorize("Catalogo", "Ler")]
    [HttpGet("product/{id}")]
    public async Task<Product> ProductDetail(Guid id)
    {
        Console.WriteLine($"Aqui no CatalogController com o id: {id}");
        return await _productRepository.GetById(id);
    }
}

// [Route("api/catalog")]
// [Authorize]
// public class CatalogController
// {
//     private readonly IProductRepository _productRepository;
//
//     public CatalogController(IProductRepository productRepository)
//     {
//         _productRepository = productRepository;
//     }
//
//     [AllowAnonymous]
//     [HttpGet("products")]
//     public async Task<IEnumerable<Product>> Index()
//     {
//         Console.WriteLine("Aqui no Controller do catalog no metodo index()");
//         return await _productRepository.GetAll();
//     }
//
//     [ClaimsAuthorize("Catalogo", "Ler")]
//     [HttpGet("product/{id}")]
//     public async Task<Product> GetById([FromRoute] Guid id)
//     {
//         Console.WriteLine("Aqui no product detail");
//         return await _productRepository.GetById(id);
//     }
