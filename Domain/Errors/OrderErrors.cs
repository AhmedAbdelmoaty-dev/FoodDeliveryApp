namespace Domain.Errors
{
    public static class OrderErrors
    {
        public static Error InvalidPrice(Guid menuItemId) =>
            new Error( code: "Order.InvalidUnitPrice", $"The unit price for menu item with ID '{menuItemId}' is invalid.");

        public static Error InvalidOrderItem(Guid menuItemId) => 
            new Error( code: "Order.InvalidOrderItem", $"The order " +
                $"item for menu item with ID '{menuItemId}' has invalid quantity or unit price.");

        public static Error NotFound(Guid id) =>
            new Error("Order.NotFound", $"Order with id {id} was not found");

        public static Error InvalidCancellation(Guid orderId) =>
            new Error("InvalidCancellation", $"Cannot cancel the order with {orderId} because its already preparing or out for delivery");

        public static Error IdentityNotMatch=new Error(ErrorCodes.Forbidden,"user not own the order ");

    }
}
