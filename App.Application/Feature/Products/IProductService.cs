using App.Application;
using App.Application.Feature.Products.Create;
using App.Application.Feature.Products.Dto;
using App.Application.Feature.Products.Update;
using App.Application.Feature.Products.UpdateStock;

namespace App.Application.Feature.Products;

public interface IProductService
{
	Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
	Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
	Task<ServiceResult<List<ProductDto>>> GetAllListAsync();
	Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageIndex, int pageSize);
	Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
	Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request);
	Task<ServiceResult> UpdateAsync(UpdateProductStockRequest request);
	Task<ServiceResult> DeleteAsync(int id);
}
