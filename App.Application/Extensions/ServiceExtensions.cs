using App.Application.Feature.Category;
using App.Application.Feature.Products;
using App.Services.Categories;
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
			

			// async olarak servis metodlarında fluent validation yapmak istersek burayı kapatıyoruz.
			services.AddFluentValidationAutoValidation();
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			services.AddAutoMapper(Assembly.GetExecutingAssembly());
		
			return services;
		}
	}
}
