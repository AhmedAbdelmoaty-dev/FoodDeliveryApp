using Domain.Abstractions.Result;
using MediatR;

namespace Application.Features.Restaurants.Commands.DeleteMenuItemImage
{
    public record DeleteMenuItemImageCommand(Guid RestaurantId, Guid MenuItemId) : IRequest<Result>;
}