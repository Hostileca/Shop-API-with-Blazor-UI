using AutoMapper;
using Shop.BL.Dtos.Category;
using Shop.BL.Services.Interfaces;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;
using System.Data;

namespace Shop.BL.Services.Implementation
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepo _categoriesRepo;
        private readonly IMapper _mapper;

        public CategoriesService(ICategoriesRepo categoriesRepo, IMapper mapper)
        {
            _categoriesRepo = categoriesRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllCategories()
        {
            var categories = await _categoriesRepo.GetAllCategories();
            return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }

        public async Task<CategoryDetailedReadDto> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            if (await _categoriesRepo.GetCategoryByName(categoryCreateDto.Name) is not null)
            {
                throw new DuplicateNameException($"Category {categoryCreateDto.Name} already exists");
            }
            var category = _mapper.Map<Category>(categoryCreateDto);
            await _categoriesRepo.AddCategory(category);
            await _categoriesRepo.SaveChanges();
            return _mapper.Map<CategoryDetailedReadDto>(category);
        }

        public async Task<CategoryDetailedReadDto> DeleteCategory(int id)
        {
            var category = await _categoriesRepo.GetCategoryById(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category not found with id:{id}");
            }
            _categoriesRepo.DeleteCategory(category);
            await _categoriesRepo.SaveChanges();
            return _mapper.Map<CategoryDetailedReadDto>(category);
        }

        public async Task<CategoryDetailedReadDto> GetCategoryById(int id)
        {
            var category = await _categoriesRepo.GetCategoryById(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category not found with id:{id}");
            }
            return _mapper.Map<CategoryDetailedReadDto>(category);
        }

        public async Task<CategoryDetailedReadDto> UpdateCategory(int id, CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _categoriesRepo.GetCategoryById(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category not found id:{id}");
            }
            var categoryModelToRepo = _mapper.Map(categoryUpdateDto, category);
            await _categoriesRepo.SaveChanges();
            return _mapper.Map<CategoryDetailedReadDto>(categoryModelToRepo);
        }
    }
}
