using Application.Features.Restaurants;
using Domain.Abstractions.Result;
using MediatR;

namespace Application.Features.Restaurants.Queries.GetRestaurant
{
    public record GetRestaurantByIdQuery(Guid Id):IRequest<Result<RestaurantDto>>;


}
