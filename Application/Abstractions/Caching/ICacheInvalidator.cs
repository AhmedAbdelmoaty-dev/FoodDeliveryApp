namespace Application.Abstractions.Caching
{
    public interface ICacheInvalidator
    {
        string[] CacheKeys { get; }
    }
}
