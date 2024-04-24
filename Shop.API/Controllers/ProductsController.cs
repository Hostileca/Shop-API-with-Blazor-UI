using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.BL.Dtos.Product;
using Shop.BL.Services.Interfaces;

namespace Shop.API.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productsService.GetAllProducts();
            return Ok(products);
        }
        [HttpGet("top-10")]
        public async Task<IActionResult> GetTop10Products()
        {
            var products = await _productsService.GetTop10Products();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productsService.GetProductById(id);
            return Ok(product);
        }

        [Authorize(Roles = "root")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDto productCreateDto)
        {
            var product = await _productsService.CreateProduct(productCreateDto);
            return Ok(product);
        }

        [Authorize(Roles = "root")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productsService.DeleteProduct(id);
            return Ok(product);
        }

        [Authorize(Roles = "root")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePorduct(int id, ProductUpdateDto productUpdateDto)
        {
            var product = await _productsService.UpdateProduct(id, productUpdateDto);
            return Ok(product);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts(string searchText)
        {
            var products = await _productsService.SearchProducts(searchText);
            return Ok(products);
        }
    }
}
