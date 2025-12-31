namespace Application.Features.Authentication.Dtos
{
    public class AuthDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public string UserId { get; set; }
        public IList<string> Roles { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
