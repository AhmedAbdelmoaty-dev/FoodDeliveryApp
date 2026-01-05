using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Domain.Entities;
using Infrastructure.Authentication;
using Infrastructure.Cache;
using Infrastructure.Data;
using Infrastructure.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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


            //services.AddScoped<RestaurantRepository>();

            //services.AddScoped<IRestaurantRepository>(provider =>
            //{
            //   var repository= provider.GetRequiredService<RestaurantRepository>();

            //   var cache = provider.GetRequiredService<IDistributedCache>();

            //    return new RestaurantCachedRepository(repository,cache);

            //});

            services.AddScoped<ICacheService, CacheService>();

            services.AddScoped<IRestaurantRepository, RestaurantRepository>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<ITagRepository, TagRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITokenService, TokenService>();

            services.AddOptions<JwtOptions>().Bind(configuration.GetSection("JWT"));

            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddUserManager<UserManager<User>>();

            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = configuration.GetConnectionString("Redis");
            });




            return services;
        }
    }
}
