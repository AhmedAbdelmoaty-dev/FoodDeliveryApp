using Application.Abstractions.Caching;
using Application.Abstractions.Services;
using Domain.Abstractions.Result;
using MediatR;

namespace Application.Behaviors
{
    public class CachingBehavior<TRequest, TResponse>(ICacheService cacheService)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest:ICacheableQuery
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
              var cachedResponse= await cacheService.GetAsync<TResponse>(request.CacheKey, cancellationToken);

                if (cachedResponse is not null)
                    return cachedResponse;

               var response = await next();
               
                if(response is not null && response is IAppResult result && result.IsSuccess)
                  await cacheService.SetAsync(request.CacheKey,response,cancellationToken,request.AbsoluteExpiration,request.SlidingExpiration);
             
            return response;
          
        }
    }
}
