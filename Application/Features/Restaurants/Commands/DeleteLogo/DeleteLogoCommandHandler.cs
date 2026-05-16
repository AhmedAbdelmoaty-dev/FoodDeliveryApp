using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Domain.Abstractions.Result;
using Domain.Errors;
using MediatR;

namespace Application.Features.Restaurants.Commands.DeleteLogo
{
    public class DeleteLogoCommandHandler(
        IFileStorageService fileStorage,
        IRestaurantRepository restaurantRepo,
        IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteLogoCommand, Result>
    {
        public async Task<Result> Handle(DeleteLogoCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantRepo.GetByIdAsync(request.RestaurantId, cancellationToken);

            if (restaurant is null)
                return Result.Failure(RestaurantErrors.NotFound(request.RestaurantId));

            var oldLogoUrl = restaurant.LogoUrl;

            if (string.IsNullOrEmpty(oldLogoUrl))
                return Result.Success();

            restaurant.LogoUrl = string.Empty;
            restaurantRepo.Update(restaurant);

            var saved = await unitOfWork.SaveChangesAsync(cancellationToken);

            if (!saved)
                return Result.Failure(Error.Persistance);

            try
            {
                await fileStorage.DeleteAsync(oldLogoUrl, cancellationToken);
            }
            catch
            {
                // Log warning but don't fail
            }

            return Result.Success();
        }
    }
}