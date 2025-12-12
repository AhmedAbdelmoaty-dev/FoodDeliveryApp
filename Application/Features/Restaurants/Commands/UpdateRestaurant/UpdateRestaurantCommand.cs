using Domain.Abstractions.Result;
using MediatR;

namespace Application.Features.Restaurants.Commands.UpdateRestaurant
{
    public record UpdateRestaurantCommand(Guid id , string Name, string Address, string LogoUrl)
        :IRequest<Result>;

}
