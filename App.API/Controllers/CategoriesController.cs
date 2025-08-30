using App.Repositories.Categories;
using App.Services.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;
using App.Services.Filters;
using App.Services.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
	public class CategoriesController(ICategoryService categoryService) : CustomBaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetCategories() => CreateActionResult(await categoryService.GetAllAsync());

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetCategoryById(int id) => CreateActionResult(await categoryService.GetByIdAsync(id));

		[HttpGet("{id:int}/products")]
		public async Task<IActionResult> GetCategoryWithProducts(int id) => CreateActionResult(await categoryService.GetCategoryWithProductsAsync(id));

		[HttpGet("products")]
		public async Task<IActionResult> GetCategoriesWithProducts() => CreateActionResult(await categoryService.GetCategoriesWithProductsAsync());

		[HttpPost]
		public async Task<IActionResult> CreateCategory(CreateCategoryRequest createCategoryRequest) => CreateActionResult(await categoryService.CreateAsync(createCategoryRequest));

		[ServiceFilter(typeof(NotFoundFilter<Category, int>))]
		[HttpPut("{id:int}")]
		public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequest updateCategoryRequest) => CreateActionResult(await categoryService.UpdateAsync(id, updateCategoryRequest));

		[ServiceFilter(typeof(NotFoundFilter<Category, int>))]
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteCategory(int id) => CreateActionResult(await categoryService.DeleteAsync(id));
	}
}
