
namespace Domain.Errors
{
    public static class RestaurantErrors
    {
        public static Error NotFound(Guid id) => new(ErrorCodes.NotFound, $"Restaurant With Id {id} was not found");

        public static Error MenuItemNotFound(Guid RestuarantId,Guid MenuItemId) => 
            new(ErrorCodes.NotFound, $"MenuItem With Id {MenuItemId} was not found for Restaurant With Id {RestuarantId}");

        public static Error TagAlreadyExist(string tagName) => 
            new(ErrorCodes.Conflict, $"Tag with name {tagName} already exists in the restaurant");
    }
}
