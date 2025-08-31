using App.Application;
using App.Application.Contracts.Persistance;
using App.Application.Feature.Products.Create;
using App.Application.Feature.Products.Dto;
using App.Application.Feature.Products.Update;
using App.Application.Feature.Products.UpdateStock;
using App.Domain.Entities;
using AutoMapper;
using System.Net;

namespace App.Application.Feature.Products;

// async validation yapabilmek için IValidator<CreateProductRequest> validator parametresi ekledik
//public class ProductService(IProductRepository productRespository, IUnitOfWork unitOfWork, IValidator<CreateProductRequest> validator): IProductService
public class ProductService(IProductRepository productRespository, IUnitOfWork unitOfWork,IMapper mapper): IProductService
{
	public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
	{
		var products = await productRespository.GetTopPriceProductsAsync(count);

		var productsAsDto= mapper.Map<List<ProductDto>>(products);
		#region Manuel Mapping
		//var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList(); 
		#endregion

		return ServiceResult<List<ProductDto>>.Success(productsAsDto);
	}

	public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
	{
		var product = await productRespository.GetByIdAsync(id);
		if (product is null)
		{
			return ServiceResult<ProductDto?>.Fail("Product not found", HttpStatusCode.NotFound);
		}

		var productAsDto = mapper.Map<ProductDto>(product);
		#region Manuel Mapping
		 // var productAsDto = new ProductDto(product.Id, product.Name, product.Price, product.Stock); 
		#endregion

		return ServiceResult<ProductDto?>.Success(productAsDto)!;
	}

	public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
	{
		var products = await productRespository.GetAllAsync();
		var productsAsDto = mapper.Map<List<ProductDto>>(products);
		#region Manuel Mapping
		//var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList(); 
		#endregion
		return ServiceResult<List<ProductDto>>.Success(productsAsDto);
	}

	public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageIndex, int pageSize)
	{
		// 1-10 => ilk 10 kayıt skip(0).Take(10)
		// 2-10 => 11-20 skip(10).Take(10)
		// 3-10 => 21-30 skip(20).Take(10)

		var products = await productRespository.GetAllPagedAsync(pageIndex, pageSize);

		var productsAsDto = mapper.Map<List<ProductDto>>(products);
		#region Manuel Mapping
		//var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList(); 
		#endregion
		return ServiceResult<List<ProductDto>>.Success(productsAsDto);
	}

	public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
	{
		//throw new CriticalException("test amaçlı exception");
		
		// 2. Yol : asenkron manuel yöntem
		// Business Codes
		// Validation
		var anyProduct = await productRespository.AnyAsync(p => p.Name.ToLower() == request.Name.ToLower());

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

		var product = mapper.Map<Product>(request);

		await productRespository.AddAsync(product);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id),$"api/products/{product.Id}");

	}

	public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
	{
		// Clean code principles
		// Before Fast fail
		// Guard Clouses

		var isProductNameExist = await productRespository.AnyAsync(p => p.Name.ToLower() == request.Name.ToLower() && p.Id!=id);

		if (isProductNameExist)
		{
			return ServiceResult.Fail($"{request.Name} has been already in db", HttpStatusCode.BadRequest);
		}

		var product = mapper.Map<Product>(request);
		product.Id = id;
		productRespository.Update(product);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult.Success(HttpStatusCode.NoContent);
	}

	public async Task<ServiceResult> UpdateAsync(UpdateProductStockRequest request)
	{
		var existingProduct = await productRespository.GetByIdAsync(request.ProductId);
		if (existingProduct is null)
		{
			return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
		}
		
		existingProduct.Stock = request.Quantity;
		productRespository.Update(existingProduct);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult.Success(HttpStatusCode.NoContent);
	}

	public async Task<ServiceResult> DeleteAsync(int id)
	{
		var product = await productRespository.GetByIdAsync(id);
		
		productRespository.Delete(product!);
		await unitOfWork.SaveChangesAsync();
		return ServiceResult.Success(HttpStatusCode.NoContent);
	}

} 


