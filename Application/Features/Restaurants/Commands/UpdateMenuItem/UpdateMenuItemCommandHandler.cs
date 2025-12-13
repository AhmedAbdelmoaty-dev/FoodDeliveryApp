using Application.Abstractions.Repositories;
using Domain.Abstractions.Result;
using Domain.Errors;
using Mapster;
using MediatR;

namespace Application.Features.Restaurants.Commands.UpdateMenuItem
{
    internal class UpdateMenuItemCommandHandler(IRestaurantRepository restaurantRepo
        , IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateMenuItemCommand, Result>
    {
        public async Task<Result> Handle(UpdateMenuItemCommand command, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantRepo.GetRestaurantWithSpecificMenuItemByIdAsync
                (command.RestaurantId, command.MenuItemId, cancellationToken);

            if(restaurant is null)
                return Result.Failure(RestaurantErrors.NotFound(command.RestaurantId));

            var menuItem = restaurant.MenuItems.SingleOrDefault(x=>x.Id==command.MenuItemId);

            if(menuItem is null)
                return Result.Failure(RestaurantErrors.MenuItemNotFound(command.RestaurantId,command.MenuItemId));

            command.Adapt(menuItem);

           var isPersisted= await unitOfWork.SaveChangesAsync(cancellationToken);

            if(!isPersisted)
                return Result.Failure(Error.Persistance);

            return Result.Success();

        }
    }
}
