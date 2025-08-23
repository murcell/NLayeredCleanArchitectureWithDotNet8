using App.Services.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
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

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, UpdateProductRequest request)=> CreateActionResult(await productService.UpdateAsync(id, request));

		[HttpPatch("stock")]
		public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) => CreateActionResult(await productService.UpdateAsync(request));

		//[HttpPut("UpdateStock")]
		//public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) => CreateActionResult(await productService.UpdateAsync(request));

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id) => CreateActionResult(await productService.DeleteAsync(id));

	}
}
