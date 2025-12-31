namespace Application.Abstractions.Services
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key,CancellationToken cancellationToken);

        Task SetAsync<T>(string key, T value, CancellationToken cancellationToken, TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpireTime=null);

        Task RemoveAsync(string key, CancellationToken cancellationToken);
    }
}
