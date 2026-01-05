using Application.Abstractions.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrastructure.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(key))
                return default;

            var serializedData= await _cache.GetStringAsync(key, cancellationToken);

           if (string.IsNullOrEmpty(serializedData))
                return default;

          var data=  JsonSerializer.Deserialize<T>(serializedData);

            return data;
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(key))
                return;

            await _cache.RemoveAsync(key, cancellationToken);
        }

        public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default, TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpireTime = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                return;

            if (value is null)
                throw new ArgumentNullException("cannot cache null value");
         
            var options = new DistributedCacheEntryOptions{
                AbsoluteExpirationRelativeToNow=absoluteExpireTime,
                SlidingExpiration=slidingExpireTime
            };

           var serializedData=  JsonSerializer.Serialize(value);

          await  _cache.SetStringAsync(key, serializedData, options, cancellationToken);
        }
    }
}
