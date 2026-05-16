using Domain.Abstractions.Result;
using MediatR;

namespace Application.Features.Restaurants.Commands.DeleteLogo
{
    public record DeleteLogoCommand(Guid RestaurantId) : IRequest<Result>;
}