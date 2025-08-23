using App.Repositories;
using App.Repositories.Products;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace App.Services.Products;

// async validation yapabilmek için IValidator<CreateProductRequest> validator parametresi ekledik
//public class ProductService(IProductRepository productRespository, IUnitOfWork unitOfWork, IValidator<CreateProductRequest> validator): IProductService
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

	public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageIndex, int pageSize)
	{
		// 1-10 => ilk 10 kayıt skip(0).Take(10)
		// 2-10 => 11-20 skip(10).Take(10)
		// 3-10 => 21-30 skip(20).Take(10)

		var query = productRespository.GetAll();
		var totalCount = await query.CountAsync();
		var products = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
		var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
		
		return ServiceResult<List<ProductDto>>.Success(productsAsDto);
	}

	public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
	{
		// 2. Yol : asenkron manuel yöntem
		// Business Codes
		// Validation
		var anyProduct = await productRespository.Where(p => p.Name.ToLower() == request.Name.ToLower())
			.AnyAsync();

		if (anyProduct)
		{
			return ServiceResult<CreateProductResponse>.Fail("Product name must be unique", HttpStatusCode.BadRequest);
		}

		//// 3. Yol : asenkron yöntem fluent validation .net core pipelinı devre dışı bırakıyoruz
		//var validationResult = await validator.ValidateAsync(request);
		//if (!validationResult.IsValid)
		//{
		//	return ServiceResult<CreateProductResponse>.Fail(validationResult.Errors.Select(e => e.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
		//}

		var product = new Product()
		{
			Name = request.Name,
			Price = request.Price,
			Stock = request.Stock
		};

		await productRespository.AddAsync(product);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id),$"api/products/{product.Id}");

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

	public async Task<ServiceResult> UpdateAsync(UpdateProductStockRequest request)
	{
		var existingProduct = await productRespository.GetByIdAsync(request.ProductId);
		if (existingProduct is null)
		{
			return ServiceResult.Fail("Product not found", System.Net.HttpStatusCode.NotFound);
		}
		
		existingProduct.Stock = request.Quantity;
		productRespository.Update(existingProduct);
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


