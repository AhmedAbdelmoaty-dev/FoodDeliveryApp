using Application.Abstractions.Repositories;
using Domain.Abstractions.Result;
using Mapster;
using MediatR;
using static Application.Specifications.RestaurantsPaginatedSpec;

namespace Application.Features.Restaurants.Queries.GetRestaurantWIthItems
{
    public class GetRestaurantWithItemQueryHandler(IRestaurantRepository restaurantRepo)
        : IRequestHandler<GetRestaurantWithItemQuery, Result<RestaurantDto>>
    {
        public async Task<Result<RestaurantDto>> Handle(GetRestaurantWithItemQuery query, CancellationToken cancellationToken)
        {
            var spec = new RestaurantWithItemsSpec(query.Id);

            var restaurants= await restaurantRepo.GetBySpecAsync(spec, cancellationToken);

            var restaurantsDto = restaurants.Adapt<RestaurantDto>();

            return Result<RestaurantDto>.Success(restaurantsDto);
        }
    }
}
