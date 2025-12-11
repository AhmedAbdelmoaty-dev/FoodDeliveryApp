using Domain.Abstractions;
using MediatR;

namespace Application.Restaurants.Commands.DeleteRestaurant
{
    public record DeleteRestaurantCommand(Guid id):IRequest<Result>;

}
