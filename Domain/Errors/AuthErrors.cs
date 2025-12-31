namespace Domain.Errors
{
    public static class AuthErrors
    {
        public static Error InvalidRefreshToken = new(ErrorCodes.Authentication, "The refresh token is invalid.");
    
        public static Error AlreadyExist(string email) => new(ErrorCodes.Conflict, $" User with email {email} already exists.");

        public static Error InvalidCredential = new Error(ErrorCodes.Authentication, "Invalid email or password");

        public static Error RegisterFailed(IList<string> errors)
        {
            var combinedErrors = string.Join("; ", errors);
            return new Error(ErrorCodes.Presistance, $"User registration failed: {combinedErrors}");
        }
    }
}
