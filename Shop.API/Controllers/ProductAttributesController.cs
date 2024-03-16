using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.BL.Dtos.Attribute;
using Shop.BL.Services.Interfaces;

namespace Shop.API.Controllers
{
    [Route("api/v1/products/{productId}/attributes")]
    [ApiController]
    public class ProductAttributesController : ControllerBase
    {
        private readonly IProductAttributesService _productAttributesService;
        public ProductAttributesController(IProductAttributesService productAttributesService)
        {
            _productAttributesService = productAttributesService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProductAttributes(int productId)
        {
            var productAttributes = await _productAttributesService.GetAllProductAttributes(productId);
            return Ok(productAttributes);
        }

        [Authorize(Roles = "root")]
        [HttpPost]
        public async Task<IActionResult> CreateProductAttribute(int productId, AttributeCreateDto attributeCreateDto)
        {
            var attributeFromRepo = await _productAttributesService.CreateProductAttribute(productId, attributeCreateDto);
            return Ok(attributeFromRepo);
        }

        [Authorize(Roles = "root")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAttribute(int productId, int id, ProductAttributeUpdateDto productAttributeUpdateDto)
        {
            var productAttributes = await _productAttributesService.UpdateProductAttribute(productId, id, productAttributeUpdateDto);
            return Ok(productAttributes);
        }

        [Authorize(Roles = "root")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAttribute(int productId, int id)
        {
            var attributeFromRepo = await _productAttributesService.DeleteProductAttribute(productId, id);
            return Ok(attributeFromRepo);
        }
    }
}
