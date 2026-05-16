using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Domain.Abstractions.Result;
using Domain.Errors;
using MediatR;

namespace Application.Features.Restaurants.Commands.UploadLogo
{
    public class UploadLogoCommandHandler(
        IFileStorageService fileStorage,
        IRestaurantRepository restaurantRepo,
        IUnitOfWork unitOfWork)
        : IRequestHandler<UploadLogoCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UploadLogoCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantRepo.GetByIdAsync(request.RestaurantId, cancellationToken);

            if (restaurant is null)
                return Result<string>.Failure(RestaurantErrors.NotFound(request.RestaurantId));

            var oldLogoUrl = restaurant.LogoUrl;

            await using var stream = new MemoryStream(request.ImageData);
            var fileUrl = await fileStorage.UploadAsync("restaurant-logos", stream, request.FileName, cancellationToken);

            restaurant.LogoUrl = fileUrl;
            restaurantRepo.Update(restaurant);

            var saved = await unitOfWork.SaveChangesAsync(cancellationToken);

            if (!saved)
            {
                await fileStorage.DeleteAsync(fileUrl, cancellationToken);
                return Result<string>.Failure(Error.Persistance);
            }

            if (!string.IsNullOrEmpty(oldLogoUrl) && oldLogoUrl != fileUrl)
            {
                try
                {
                    await fileStorage.DeleteAsync(oldLogoUrl, cancellationToken);
                }
                catch
                {
                    // Log warning but don't fail the request
                }
            }

            return Result<string>.Success(fileUrl);
        }
    }
}