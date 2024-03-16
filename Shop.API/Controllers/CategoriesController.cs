using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.BL.Dtos.Category;
using Shop.BL.Services.Interfaces;

namespace Shop.API.Controllers
{
    [Route("api/v1/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;
        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoriesService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoriesService.GetCategoryById(id);
            return Ok(category);
        }

        [Authorize(Roles = "root")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            var category = await _categoriesService.CreateCategory(categoryCreateDto);
            return Ok(category);
        }

        [Authorize(Roles = "root")]
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _categoriesService.UpdateCategory(categoryId, categoryUpdateDto);
            return Ok(category);
        }

        [Authorize(Roles = "root")]
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var category = await _categoriesService.DeleteCategory(categoryId);
            return Ok(category);
        }
    }
}
