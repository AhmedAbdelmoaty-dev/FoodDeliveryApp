using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Domain.Abstractions.Result;
using Domain.Errors;
using MediatR;

namespace Application.Features.Restaurants.Commands.DeleteMenuItemImage
{
    public class DeleteMenuItemImageCommandHandler(
        IFileStorageService fileStorage,
        IRestaurantRepository restaurantRepo,
        IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteMenuItemImageCommand, Result>
    {
        public async Task<Result> Handle(DeleteMenuItemImageCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantRepo.GetRestaurantWithSpecificMenuItemByIdAsync(
                request.RestaurantId, request.MenuItemId, cancellationToken);

            if (restaurant is null)
                return Result.Failure(RestaurantErrors.NotFound(request.RestaurantId));

            var menuItem = restaurant.MenuItems.FirstOrDefault(m => m.Id == request.MenuItemId);

            if (menuItem is null)
                return Result.Failure(MenuItemErrors.NotFound(request.MenuItemId));

            var oldImageUrl = menuItem.ImageUrl;

            if (string.IsNullOrEmpty(oldImageUrl))
                return Result.Success();

            menuItem.ImageUrl = string.Empty;

            var saved = await unitOfWork.SaveChangesAsync(cancellationToken);

            if (!saved)
                return Result.Failure(Error.Persistance);

            try
            {
                await fileStorage.DeleteAsync(oldImageUrl, cancellationToken);
            }
            catch
            {
                // Log warning but don't fail
            }

            return Result.Success();
        }
    }
}