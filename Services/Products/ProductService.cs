using App.Repositories;
using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Services.Products;

public class ProductService(IProductRepository productRespository, IUnitOfWork unitOfWork): IProductService
{
	public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
	{
		var products = await productRespository.GetTopPriceProductsAsync(count);
		var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

		return ServiceResult<List<ProductDto>>.Success(productsAsDto);
	}

	public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
	{
		var product = await productRespository.GetByIdAsync(id);
		if (product is null)
		{
			return ServiceResult<ProductDto?>.Fail("Product not found", System.Net.HttpStatusCode.NotFound);
		}

		var productAsDto = new ProductDto(product.Id, product.Name, product.Price, product.Stock);

		return ServiceResult<ProductDto?>.Success(productAsDto)!;
	}

	public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
	{
		var products = await productRespository.GetAll().ToListAsync();
		var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
		return ServiceResult<List<ProductDto>>.Success(productsAsDto);
	}

	public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
	{
		var product = new Product()
		{
			Name = request.Name,
			Price = request.Price,
			Stock = request.Stock
		};

		await productRespository.AddAsync(product);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult<CreateProductResponse>.Success(new CreateProductResponse(product.Id));
	}

	public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
	{
		// Clean code principles
		// Before Fast fail
		// Guard Clouses
		var product = await productRespository.GetByIdAsync(id);
		if (product is null)
		{
			return ServiceResult.Fail("Product not found", System.Net.HttpStatusCode.NotFound);
		}

		product.Name = request.Name;
		product.Price = request.Price;
		product.Stock = request.Stock;

		productRespository.Update(product);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult.Success(HttpStatusCode.NoContent);
	}

	public async Task<ServiceResult> DeleteAsync(int id)
	{
		var product = await productRespository.GetByIdAsync(id);
		if (product is null)
		{
			return ServiceResult.Fail("Product not found", System.Net.HttpStatusCode.NotFound);
		}
		productRespository.Delete(product);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult.Success(HttpStatusCode.NoContent);
	}

} 


