using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.BL.Dtos.Manufacturer;
using Shop.BL.Services.Implementation;
using Shop.BL.Services.Interfaces;

namespace Shop.API.Controllers
{
    [Route("api/v1/manufacturers")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturersService _manufacturersService;
        public ManufacturersController(IManufacturersService manufacturersService)
        {
            _manufacturersService = manufacturersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllManufacturers()
        {
            var manufacturers = await _manufacturersService.GetAllManufacturers();
            return Ok(manufacturers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetManufacturerById(int id)
        {
            var manufacturer = await _manufacturersService.GetManufacturerById(id);
            return Ok(manufacturer);
        }

        [Authorize(Roles = "root")]
        [HttpPost]
        public async Task<IActionResult> CreateManufacturer(ManufacturerCreateDto manufacturerCreateDto)
        {
            var manufacturer = await _manufacturersService.CreateManufacturer(manufacturerCreateDto);
            return Ok(manufacturer);
        }

        [Authorize(Roles = "root")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManufacturer(int id, ManufacturerUpdateDto manufacturerUpdateDto)
        {
            var manufacturer = await _manufacturersService.UpdateManufacturer(id, manufacturerUpdateDto);
            return Ok(manufacturer);
        }

        [Authorize(Roles = "root")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManufacturer(int id)
        {
            var manufacturer = await _manufacturersService.DeleteManufacturer(id);
            return Ok(manufacturer);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchManufacturers(string searchText)
        {
            var manufacturers = await _manufacturersService.SearchManufacturers(searchText);
            return Ok(manufacturers);
        }
    }
}
