using App.Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Caching;

public class CacheService(IMemoryCache memoryCache) : ICacheService
{
	public Task<T?> GetAsync<T>(string cacheKey)
	{
		if (memoryCache.TryGetValue(cacheKey, out var cacheItem))
		{
			return Task.FromResult((T?)cacheItem);
		}
		return Task.FromResult(default(T));
	}

	public Task AddAsync<T>(string cacheKey, T value, TimeSpan exprTimeSpan)
	{

		var cacheEntryOptions = new MemoryCacheEntryOptions()
		{
			AbsoluteExpirationRelativeToNow = exprTimeSpan
		};

		memoryCache.Set(cacheKey, value, cacheEntryOptions);
		return Task.CompletedTask;

	}
	
	public Task RemoveAsync(string cacheKey)
	{
		memoryCache.Remove(cacheKey);
		return Task.CompletedTask;
	}
}
