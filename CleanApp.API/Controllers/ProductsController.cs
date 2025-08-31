using App.Application.Feature.Products;
using App.Application.Feature.Products.Create;
using App.Application.Feature.Products.Update;
using App.Application.Feature.Products.UpdateStock;
using App.Domain.Entities;
using CleanApp.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.API.Controllers;

public class ProductsController(IProductService productService) : CustomBaseController
{
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		return CreateActionResult(await productService.GetAllListAsync());
	}

	[HttpGet("{pageIndex:int}/{pageSize:int}")]
	public async Task<IActionResult> GetPagedAllList(int pageIndex, int pageSize) => CreateActionResult(await productService.GetPagedAllListAsync(pageIndex, pageSize));

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetById(int id) => CreateActionResult(await productService.GetByIdAsync(id));

	//public async Task<IActionResult> GetTopPriceProducts(int count) => CreateActionResult(await productService.GetTopPriceProductsAsync(count));

	[HttpPost]
	public async Task<IActionResult> Create(CreateProductRequest request) => CreateActionResult(await productService.CreateAsync(request));

	[ServiceFilter(typeof(NotFoundFilter<Product, int>))]
	[HttpPut("{id:int}")]
	public async Task<IActionResult> Update(int id, UpdateProductRequest request)=> CreateActionResult(await productService.UpdateAsync(id, request));

	[HttpPatch("stock")]
	public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) => CreateActionResult(await productService.UpdateAsync(request));

	//[HttpPut("UpdateStock")]
	//public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) => CreateActionResult(await productService.UpdateAsync(request));

	[ServiceFilter(typeof(NotFoundFilter<Product,int>))]
	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id) => CreateActionResult(await productService.DeleteAsync(id));

}
