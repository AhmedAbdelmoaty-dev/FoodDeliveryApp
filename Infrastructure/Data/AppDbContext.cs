using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<MenuItem>MenuItems { get; set; }   
    }
}
