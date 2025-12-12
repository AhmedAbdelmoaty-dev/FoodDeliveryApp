using Application.Abstractions.Repositories;
using Application.Features.Restaurants;
using Domain.Abstractions.Result;
using Domain.Errors;
using Mapster;
using MediatR;

namespace Application.Features.Restaurants.Queries.GetRestaurant
{
    internal class GetRestaurantByIdQueryHandler(IRestaurantRepository restaurantRepo) 
        : IRequestHandler<GetRestaurantByIdQuery, Result<RestaurantDto>>
    {
        public async Task<Result<RestaurantDto>> Handle(GetRestaurantByIdQuery command, CancellationToken cancellationToken)
        {
           var restaurant = await restaurantRepo.GetByIdAsync(command.Id, cancellationToken);

            if (restaurant == null)
                return Result<RestaurantDto>.Failure(RestaurantErrors.NotFound(command.Id));

            var restaurantDto = restaurant.Adapt<RestaurantDto>();

            return Result<RestaurantDto>.Success(restaurantDto);
        }
    }
}
