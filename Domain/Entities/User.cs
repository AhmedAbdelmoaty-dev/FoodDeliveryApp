
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public  class User : IdentityUser
    {
        public string? FirstName { get; set; }
 
        public string? LastName { get; set; }

        public string? Address { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; } = new();
    }
}
