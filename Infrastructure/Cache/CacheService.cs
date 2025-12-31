using Application.Abstractions.Services;

namespace Infrastructure.Cache
{
    public class CacheService : ICacheService
    {

        public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync<T>(string key, T value, CancellationToken cancellationToken, TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpireTime = null)
        {
            throw new NotImplementedException();
        }
    }
}
