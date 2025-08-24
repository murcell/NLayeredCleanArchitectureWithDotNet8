using App.Services.Categories;
using App.Services.ExceptionHangler;
using App.Services.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Services.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<ICategoryService, CategoryService>();
			// async olarak servis metodlarında fluent validation yapmak istersek burayı kapatıyoruz.
			services.AddFluentValidationAutoValidation();
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddExceptionHandler<CriticalExceptionHandler>();
			services.AddExceptionHandler<GlobalExceptionHandler>();
			return services;
		}
	}
}
