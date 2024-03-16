using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.BL.Dtos.Attribute;
using Shop.BL.Services.Interfaces;

namespace Shop.API.Controllers
{
    [Route("api/v1/attributes/")]
    [ApiController]
    public class AttributesController : ControllerBase
    {
        private readonly IAttributesService _attributesService;
        public AttributesController(IAttributesService attributesService)
        {
            _attributesService = attributesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttributes()
        {
            var attributeFromRepo = await _attributesService.GetAllAttributes();
            return Ok(attributeFromRepo);
        }

        [Authorize(Roles = "root")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttribute(int id, AttributeUpdateDto attributeUpdateDto)
        {
            var attributeFromRepo = await _attributesService.UpdateAttribute(id, attributeUpdateDto);
            return Ok(attributeFromRepo);
        }

        [Authorize(Roles = "root")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttribute(int id)
        {
            var attributeFromRepo = await _attributesService.DeleteAttribute(id);
            return Ok(attributeFromRepo);
        }
    }
}
