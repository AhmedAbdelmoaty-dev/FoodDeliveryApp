using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Domain.Abstractions.Result;
using Domain.Errors;
using MediatR;

namespace Application.Features.Restaurants.Commands.UploadMenuItemImage
{
    public class UploadMenuItemImageCommandHandler(
        IFileStorageService fileStorage,
        IRestaurantRepository restaurantRepo,
        IUnitOfWork unitOfWork)
        : IRequestHandler<UploadMenuItemImageCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UploadMenuItemImageCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantRepo.GetRestaurantWithSpecificMenuItemByIdAsync(
                request.RestaurantId, request.MenuItemId, cancellationToken);

            if (restaurant is null)
                return Result<string>.Failure(RestaurantErrors.NotFound(request.RestaurantId));

            var menuItem = restaurant.MenuItems.FirstOrDefault(m => m.Id == request.MenuItemId);

            if (menuItem is null)
                return Result<string>.Failure(MenuItemErrors.NotFound(request.MenuItemId));

            var oldImageUrl = menuItem.ImageUrl;

            await using var stream = new MemoryStream(request.ImageData);
            var fileUrl = await fileStorage.UploadAsync("menu-items", stream, request.FileName, cancellationToken);

            menuItem.ImageUrl = fileUrl;

            var saved = await unitOfWork.SaveChangesAsync(cancellationToken);

            if (!saved)
            {
                await fileStorage.DeleteAsync(fileUrl, cancellationToken);
                return Result<string>.Failure(Error.Persistance);
            }

            if (!string.IsNullOrEmpty(oldImageUrl) && oldImageUrl != fileUrl)
            {
                try
                {
                    await fileStorage.DeleteAsync(oldImageUrl, cancellationToken);
                }
                catch
                {
                    // Log warning but don't fail
                }
            }

            return Result<string>.Success(fileUrl);
        }
    }
}