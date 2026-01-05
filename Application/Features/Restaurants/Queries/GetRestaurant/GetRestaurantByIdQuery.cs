using Application.Abstractions.Caching;
using Application.Common;
using Application.Features.Restaurants;
using Domain.Abstractions.Result;
using MediatR;

namespace Application.Features.Restaurants.Queries.GetRestaurant
{
    public record GetRestaurantByIdQuery(Guid Id)
        : IRequest<Result<RestaurantDto>>, ICacheableQuery
    {
        public string CacheKey => CacheFactory.RestaurantByIdKey(Id);

        public TimeSpan AbsoluteExpiration => CacheFactory.AbsoluteExpiration;

        public TimeSpan SlidingExpiration => CacheFactory.SlidingExpiration;
    }
}
