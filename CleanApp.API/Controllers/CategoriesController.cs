using App.Application.Feature.Category;
using App.Application.Feature.Category.Update;
using App.Application.Feature.Category.Create;
using App.Domain.Entities;
using CleanApp.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.API.Controllers;

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
