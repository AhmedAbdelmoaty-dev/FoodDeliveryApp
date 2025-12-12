
namespace Domain.Errors
{
    public static class RestaurantErrors
    {
        public static Error NotFound(Guid id) => new(ErrorCodes.NotFound, $"Restaurant With Id {id} was not found");
    }
}
