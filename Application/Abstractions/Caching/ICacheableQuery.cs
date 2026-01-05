namespace Application.Abstractions.Caching
{
    public interface ICacheableQuery
    {
        string CacheKey { get; }    
        TimeSpan AbsoluteExpiration { get; }
        TimeSpan SlidingExpiration { get; }   
    }
}
