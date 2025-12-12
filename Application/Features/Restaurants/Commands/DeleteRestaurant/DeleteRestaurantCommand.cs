using Domain.Abstractions.Result;
using MediatR;

namespace Application.Features.Restaurants.Commands.DeleteRestaurant
{
    public record DeleteRestaurantCommand(Guid id):IRequest<Result>;

}
