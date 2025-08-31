using App.Application.Contracts.Persistance;
using App.Domain.Entities.Common;
using App.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Persistence;

public class GenericRepository<T,TId>(AppDbContext context) : IGenericRepository<T,TId> where T : BaseEntity<TId> where TId : struct
{
	protected AppDbContext Context { get; } = context ?? throw new ArgumentNullException(nameof(context));
	private readonly DbSet<T> _dbSet = context.Set<T>();
	public IQueryable<T> GetAll()=> _dbSet.AsQueryable().AsNoTracking();
	//public IQueryable<T> GetAll()
	//{
	//	return _dbSet.AsQueryable();
	//}
	public IQueryable<T> Where(Expression<Func<T, bool>> predicate)=> _dbSet.Where(predicate).AsNoTracking();

	public Task<bool> AnyAsync(TId id) => _dbSet.AnyAsync(x => x.Id.Equals(id));

	public ValueTask<T?> GetByIdAsync(int id)=> _dbSet.FindAsync(id);

	public async ValueTask AddAsync(T entity)=> await _dbSet.AddAsync(entity);

	public void Update(T entity)=> _dbSet.Update(entity);

	public void Delete(T entity)=> _dbSet.Remove(entity);

	public Task<List<T>> GetAllAsync()
	{
		return _dbSet.AsNoTracking().ToListAsync();
	}

	public Task<List<T>> GetAllPagedAsync(int pageIndex, int pageSize)
	{
		return _dbSet.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
	}

	public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
	{
		return _dbSet.AnyAsync(predicate);
	}
}

