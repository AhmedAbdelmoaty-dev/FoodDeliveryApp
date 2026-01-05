using Application.Abstractions.Caching;
using Application.Abstractions.Services;
using Domain.Abstractions.Result;
using MediatR;

namespace Application.Behaviors
{
    public class CacheInvalidatorBehavior<TRequest, TResponse>(ICacheService cacheService)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest:ICacheInvalidator
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
          var response=   await next();

            if(response is IAppResult result && result.IsSuccess)
            {
          
              foreach(var key in request.CacheKeys)
              {
                 await cacheService.RemoveAsync(key,cancellationToken);
              }

            }

            return response;
        }
    }
}
