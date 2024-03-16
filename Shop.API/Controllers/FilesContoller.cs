using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.BL.Services.Interfaces;

namespace Shop.API.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class FilesContoller : ControllerBase
    {
        private readonly IFilesService _filesService;
        public FilesContoller(IFilesService filesService)
        {
            _filesService = filesService;
        }

        [Authorize(Roles = "root")]
        [HttpPost("products/{productId}/images")]
        public async Task<IActionResult> UploadProductImage(int productId, IFormFile file)
        {
            return Ok(await _filesService.UploadProductImage(productId, file));
        }

        [Authorize(Roles = "root")]
        [HttpPost("categories/{categoryId}/images")]
        public async Task<IActionResult> UploadCategoryImage(int categoryId, IFormFile file)
        {
            return Ok(await _filesService.UploadCategoryImage(categoryId, file));
        }

        [Authorize(Roles = "root")]
        [HttpPost("manufacturers/{manufacturerId}/images")]
        public async Task<IActionResult> UploadManufacturerImage(int manufacturerId, IFormFile file)
        {
            return Ok(await _filesService.UploadManufacturerImage(manufacturerId, file));
        }

        [HttpGet("products/images/{productImageId}")]
        public async Task<IActionResult> LoadProductImage(int productImageId)
        {
            var result = await _filesService.GetProductImageById(productImageId);
            return File(result, "image/jpg");
        }

        [HttpGet("categories/images/{categoryImageId}")]
        public async Task<IActionResult> LoadCategoryImage(int categoryImageId)
        {
            var result = await _filesService.GetCategoryImageById(categoryImageId);
            return File(result, "image/jpg");
        }

        [HttpGet("manufacturers/images/{manufacturerImageId}")]
        public async Task<IActionResult> LoadManufacturerImage(int manufacturerImageId)
        {
            var result = await _filesService.GetManufactuerImageById(manufacturerImageId);
            return File(result, "image/jpg");
        }

        [Authorize(Roles = "root")]
        [HttpDelete("products/images/{productImageId}")]
        public async Task<IActionResult> DeleteProductImage(int productImageId)
        {
            return Ok(await _filesService.DeleteProductImage(productImageId));
        }

        [Authorize(Roles = "root")]
        [HttpDelete("categories/images/{categoryImageId}")]
        public async Task<IActionResult> DeleteCategoryImage(int categoryImageId)
        {
            return Ok(await _filesService.DeleteCategoryImage(categoryImageId));
        }

        [Authorize(Roles = "root")]
        [HttpDelete("manufacturers/images/{manufacturerImageId}")]
        public async Task<IActionResult> DeleteManufacturerImage(int manufacturerImageId)
        {
            return Ok(await _filesService.DeleteManufacturerImage(manufacturerImageId));
        }
    }
}
