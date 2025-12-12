namespace Domain.Errors
{
    public static class TagErrors
    {
        public static Error NotFound(Guid id) => new(ErrorCodes.NotFound, $"Tag with Id {id} was not found");
    }
}
