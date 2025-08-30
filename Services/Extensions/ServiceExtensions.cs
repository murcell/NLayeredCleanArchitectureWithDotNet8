using App.Services.Categories;
using App.Services.ExceptionHangler;
using App.Services.Filters;
using App.Services.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Services.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<ApiBehaviorOptions>(options =>
			{
				// .net default hatasını kapatıyoruz kendi hatamızı döndürüyoruz
				options.SuppressModelStateInvalidFilter = true;
			});
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped(typeof(NotFoundFilter<,>));

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
