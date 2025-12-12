namespace Domain.Errors
{
    public record Error
    {
        public string Code { get; }
        public string Message { get; }

        internal Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public static Error None = new(string.Empty, string.Empty);

        public static Error Persistance = new(ErrorCodes.Presistance, "Unable to Persist Write operation");

        public static Error GetValidation(string errors) => new(ErrorCodes.Validation, errors);

    }

    public static class ErrorCodes
    {
        public const string NotFound = "Not_Found";
        public const string BadRequest = "Bad_Request";
        public const string Presistance = "Presistance";
        public const string Forbidden = "Forbidden";
        public const string Conflict = "Conflict";
        public const string Validation = "Validation";

    }

}
