using App.Repositories.Products;
using App.Services.Filters;
using App.Services.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
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
}
