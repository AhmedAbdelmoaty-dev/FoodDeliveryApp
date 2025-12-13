using Application.Abstractions.Repositories;
using Domain.Abstractions.Result;
using Domain.Errors;
using MediatR;

namespace Application.Features.Restaurants.Commands.DeleteMenuItem
{
    internal class DeleteMenuItemCommandHandler(IRestaurantRepository restaurantRepo
        , IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteMenuItemCommand, Result>
    {
        public async Task<Result> Handle(DeleteMenuItemCommand command, CancellationToken cancellationToken)
        {
           var restaurant= await restaurantRepo.GetRestaurantWithSpecificMenuItemByIdAsync
                                 (command.RestaurantId, command.MenuItemId, cancellationToken);

            if (restaurant is null)
                return Result.Failure(RestaurantErrors.NotFound(command.RestaurantId));

            var menuItem = restaurant.MenuItems.SingleOrDefault(x => x.Id == command.MenuItemId);

            if (menuItem is null)
                return Result.Failure(RestaurantErrors.MenuItemNotFound(command.RestaurantId, command.MenuItemId));

            restaurant.MenuItems.Remove(menuItem);

            var isPersisted =  await unitOfWork.SaveChangesAsync(cancellationToken);
             
            if (!isPersisted)
                    return Result.Failure(Error.Persistance);
    
                return Result.Success();
        }
    }
}
