using App.Application.Feature.Category.Create;
using App.Application.Feature.Category.Dto;
using App.Application.Feature.Category.Update;

namespace App.Application.Feature.Category;

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
