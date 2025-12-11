namespace Application.Common
{
    public static class CacheKeys
    {
        public static string RestaurantsBySpec (string fragments)=> $"Restaurant_List_{fragments}";

        public static string RestaurantById(Guid id) => $"Restaurant:{id}";

    }
}
