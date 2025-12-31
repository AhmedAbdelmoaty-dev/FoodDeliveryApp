using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext :IdentityDbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<MenuItem>MenuItems { get; set; }   

        public DbSet<Tag>Tags { get; set; } 

        public DbSet<Order> Orders { get; set; }
        }
}
