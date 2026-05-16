namespace Domain.Errors
{
    public static class MenuItemErrors
    {
        public static Error NotFound(Guid id) => new(ErrorCodes.NotFound, $"MenuItem with ID {id} was not found");
    }
}