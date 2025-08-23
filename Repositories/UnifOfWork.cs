namespace App.Repositories;

public class UnifOfWork(AppDbContext context) : IUnitOfWork
{
	public Task<int> SaveChangesAsync()=> context.SaveChangesAsync();

}
