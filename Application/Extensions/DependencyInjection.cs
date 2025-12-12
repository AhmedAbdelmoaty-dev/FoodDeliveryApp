using Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => {
                  cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                  cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                  cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
            
            return services;
        }
    }
}
