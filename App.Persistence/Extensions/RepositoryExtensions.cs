using App.Application.Contracts.Persistance;
using App.Domain.Options;
using App.Persistence.Categories;
using App.Persistence.Interceptors;
using App.Persistence.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Persistence.Extensions;

public static class RepositoryExtensions
{
	public static IServiceCollection AddRepositories(this IServiceCollection services,IConfiguration configuration)
	{
		services.AddDbContext<AppDbContext>(options =>
		{
			var conStr = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();

			options.UseSqlServer(conStr!.SqlServer, sqlServerOptionsAction =>
			{
				sqlServerOptionsAction.MigrationsAssembly(typeof(PersistanceAssembly).Assembly.FullName);
			});

			options.AddInterceptors(new AuditDbContextInterceptor());
		});

		services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<IUnitOfWork, UnifOfWork>();
		services.AddScoped<ICategoryRepository, CategoryRepository>();
		return services;
	}
}
