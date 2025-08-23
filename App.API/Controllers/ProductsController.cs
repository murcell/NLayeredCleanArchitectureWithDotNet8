using App.Services.Products;
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

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id) => CreateActionResult(await productService.GetByIdAsync(id));


		//public async Task<IActionResult> GetTopPriceProducts(int count) => CreateActionResult(await productService.GetTopPriceProductsAsync(count));

		[HttpPost]
		public async Task<IActionResult> Create(CreateProductRequest request) => CreateActionResult(await productService.CreateAsync(request));

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, UpdateProductRequest request)=> CreateActionResult(await productService.UpdateAsync(id, request));
		
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id) => CreateActionResult(await productService.DeleteAsync(id));

	}
}
