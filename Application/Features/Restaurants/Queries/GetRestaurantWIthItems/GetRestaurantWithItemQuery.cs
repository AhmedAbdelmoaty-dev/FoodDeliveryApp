using Application.Abstractions.Caching;
using Application.Common;
using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Restaurants.Queries.GetRestaurantWIthItems
{
    public record GetRestaurantWithItemQuery(Guid Id) : IRequest<Result<RestaurantDto>>, ICacheableQuery
    {
        public string CacheKey => CacheFactory.RestaurantByIdKey(Id);

        public TimeSpan AbsoluteExpiration => CacheFactory.AbsoluteExpiration;

        public TimeSpan SlidingExpiration => CacheFactory.SlidingExpiration;
    }

    public class GetRestaurantWithItemQueryValidator : AbstractValidator<GetRestaurantWithItemQuery>
    {
        public GetRestaurantWithItemQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Restaurant Id cannot be empty");
        }
    }

}
