namespace Application.Common
{
    public static class CacheFactory
    {

        public static string RestaurantByIdKey(Guid id) => $"Restaurant:{id}";

        public static readonly TimeSpan AbsoluteExpiration =  TimeSpan.FromMinutes(30);

        public static readonly TimeSpan SlidingExpiration = TimeSpan.FromMinutes(5);

    }
}
