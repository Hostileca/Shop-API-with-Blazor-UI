using Shop.BL.Dtos.Category;

namespace Shop.BL.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<IEnumerable<CategoryReadDto>> GetAllCategories();
        Task<CategoryDetailedReadDto> GetCategoryById(int id);
        Task<CategoryDetailedReadDto> CreateCategory(CategoryCreateDto categoryCreateDto);
        Task<CategoryDetailedReadDto> DeleteCategory(int id);
        Task<CategoryDetailedReadDto> UpdateCategory(int id, CategoryUpdateDto categoryUpdateDto);
        Task<IEnumerable<CategoryReadDto>> SearchCategories(string searchText);
    }
}
