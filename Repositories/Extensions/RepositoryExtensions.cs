using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Repositories.Extensions;

public static class RepositoryExtensions
{
	public static IServiceCollection AddRepositories(this IServiceCollection services,IConfiguration configuration)
	{
		services.AddDbContext<AppDbContext>(options =>
		{
			var conStr = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();

			options.UseSqlServer(conStr!.SqlServer, sqlServerOptionsAction =>
			{
				sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
			});
		});

		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<IUnitOfWork, UnifOfWork>();

		return services;
	}
}
