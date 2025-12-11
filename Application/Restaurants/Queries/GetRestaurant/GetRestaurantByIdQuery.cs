using Domain.Abstractions;
using MediatR;

namespace Application.Restaurants.Queries.GetRestaurant
{
    public record GetRestaurantByIdQuery(Guid Id):IRequest<Result<RestaurantDto>>;


}
