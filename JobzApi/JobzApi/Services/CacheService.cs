using JobzApi.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace JobzApi.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        public CacheService(IMemoryCache cache) => _cache = cache;

        public T Get<T>(string key)
        {
            return (T)_cache.Get(key);
        }

        public void Set<T>(string key, T value, TimeSpan expirationTime)
        {
            _cache.Set(key, value, DateTimeOffset.Now.Add(expirationTime));
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }

}
