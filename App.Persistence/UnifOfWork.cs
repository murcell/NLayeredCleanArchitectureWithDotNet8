using App.Application.Contracts.Persistance;

namespace App.Persistence;

public class UnifOfWork(AppDbContext context) : IUnitOfWork
{
	public Task<int> SaveChangesAsync()=> context.SaveChangesAsync();

}
