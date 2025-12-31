using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.OwnsMany(x => x.RefreshTokens, rt =>
            {
                rt.WithOwner().HasForeignKey("UserId");
                rt.ToTable("UserRefreshTokens");

            });
        }
            
    }
}
