using Application.Abstractions.Repositories;
using Infrastructure.Data;
using Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("DbConnection"));
            });


            services.AddScoped<RestaurantRepository>();

            services.AddScoped<IRestaurantRepository>(provider =>
            {
               var repository= provider.GetRequiredService<RestaurantRepository>();

               var cache = provider.GetRequiredService<IDistributedCache>();
               
                return new RestaurantCachedRepository(repository,cache);

            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();  

            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = configuration.GetConnectionString("Redis");
            });

            return services;
        }
    }
}
