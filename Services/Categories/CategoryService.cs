using App.Repositories;
using App.Repositories.Categories;
using App.Repositories.Products;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;
using App.Services.Products.Create;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;

namespace App.Services.Categories;

public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
{
	public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest createCategoryRequest)
	{
		var anyCategory = await categoryRepository.Where(p => p.Name.ToLower() == createCategoryRequest.Name.ToLower()).AnyAsync();

		if (anyCategory)
		{
			return ServiceResult<int>.Fail("Product name must be unique", HttpStatusCode.BadRequest);
		}


		var category = mapper.Map<Category>(createCategoryRequest);
		await categoryRepository.AddAsync(category);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult<int>.SuccessAsCreated(category.Id,$"api/categpries/{category.Id}");
	}

	public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest updateCategoryRequest)
	{
		
		var anyCategory = await categoryRepository.Where(p => p.Name.ToLower() == updateCategoryRequest.Name.ToLower() && p.Id != id).AnyAsync();

		if (anyCategory)
		{
			return ServiceResult.Fail("Category name must be unique", HttpStatusCode.BadRequest);
		}
		var category = mapper.Map<Category>(updateCategoryRequest);

		categoryRepository.Update(category);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult.Success(HttpStatusCode.NoContent);
	}

	public async Task<ServiceResult> DeleteAsync(int id)
	{
		var category = await categoryRepository.GetByIdAsync(id);
		
		categoryRepository.Delete(category!);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult.Success(HttpStatusCode.NoContent);
	}

	public async Task<ServiceResult<List<CategoryDto>>> GetAllAsync()
	{
		var categories = await categoryRepository.GetAll().ToListAsync();
		var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);
		return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
	}

	public async Task<ServiceResult<CategoryDto?>> GetByIdAsync(int id){
		var category = await categoryRepository.GetByIdAsync(id);
		if (category == null)
		{
			return ServiceResult<CategoryDto?>.Fail("Category not found", HttpStatusCode.NotFound);
		}
		var categoryAsDto = mapper.Map<CategoryDto>(category);
		return ServiceResult<CategoryDto?>.Success(categoryAsDto);
	}

	public async Task<ServiceResult<CategoryWithProductsDto?>> GetCategoryWithProductsAsync(int categoryId)
	{
		var category = await categoryRepository.GetCategoryWithProductsAsync(categoryId);
		if (category == null)
		{
			return ServiceResult<CategoryWithProductsDto?>.Fail("Category not found", HttpStatusCode.NotFound);
		}
		var categoryAsDto = mapper.Map<CategoryWithProductsDto>(category);
		return ServiceResult<CategoryWithProductsDto?>.Success(categoryAsDto);
	}

	public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoriesWithProductsAsync()
	{
		var categories = await categoryRepository.GetCategoriesWithProducts().ToListAsync();
		var categoriesAsDto = mapper.Map<List<CategoryWithProductsDto>>(categories);
		return ServiceResult<List<CategoryWithProductsDto>>.Success(categoriesAsDto);
	}
	
}
