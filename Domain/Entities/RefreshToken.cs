using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool IsExipired => DateTime.UtcNow >= ExpiryTime;
        public DateTime CreatedAt { get; set; }
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn == null && !IsExipired;
    }
}
