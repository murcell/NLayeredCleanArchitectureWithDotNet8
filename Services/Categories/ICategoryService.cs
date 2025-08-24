using App.Repositories.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;

namespace App.Services.Categories;

public interface ICategoryService
{
	Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest createCategoryRequest);
	Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest updateCategoryRequest);
	Task<ServiceResult> DeleteAsync(int id);
	Task<ServiceResult<List<CategoryDto>>> GetAllAsync();
	Task<ServiceResult<CategoryDto?>> GetByIdAsync(int id);
	Task<ServiceResult<CategoryWithProductsDto?>> GetCategoryWithProductsAsync(int categoryId);
	Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoriesWithProductsAsync();
}
