namespace Domain.Errors
{
    public record Error
    {
        public string Code { get; }
        public string Message { get; }  

        internal Error (string code, string message)
        {
            Code = code;
            Message = message;
        }

        public static Error None = new(string.Empty, string.Empty);

        public static Error Persistance = new("Persistance", "Unable to Persist Write operation");

    }
}
