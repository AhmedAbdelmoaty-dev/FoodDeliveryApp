namespace Application.Abstractions.Services
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key,CancellationToken cancellationToken = default);

        Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default, TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpireTime=null);

        Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    }
}
