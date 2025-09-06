using App.Application.Contracts.Caching;
using App.Application.Feature.Category;
using App.Application.Feature.Products;
using App.Caching;
using App.Services.Categories;
using CleanApp.API.ExceptionHandler;
using CleanApp.API.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Services.Extensions
{
	public static class CachingExtensions
	{
		public static IServiceCollection AddCachingExt(this IServiceCollection services)
		{
			services.AddMemoryCache();
			services.AddSingleton<ICacheService, CacheService>();

			return services;
		}
	}
}
