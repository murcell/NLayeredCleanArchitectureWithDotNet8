using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Categories;

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

}
