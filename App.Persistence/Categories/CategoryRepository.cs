using App.Application.Contracts.Persistance;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Categories;

public class CategoryRepository(AppDbContext context) : GenericRepository<Category, int>(context), ICategoryRepository
{
	public async Task<Category?> GetCategoryWithProductsAsync(int id)
	{
		return await Context.Categories.Include(c => c.Products).FirstOrDefaultAsync(x=>x.Id==id);
	}

	public IQueryable<Category?> GetCategoriesWithProducts()
	{
		return Context.Categories.Include(c => c.Products).AsQueryable(); 
	}

	public Task<List<Category?>> GetCategoriesWithProductsAsync()
	{
		return Context.Categories.Include(c => c.Products).Cast<Category?>().ToListAsync();
	}
}
