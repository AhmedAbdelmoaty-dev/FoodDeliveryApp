using Application.Abstractions.Repositories;
using Domain.Abstractions.Result;
using Domain.Errors;
using MediatR;

namespace Application.Features.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(IRestaurantRepository restaurantRepo,
        IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateRestaurantCommand, Result>
    {
        public async Task<Result> Handle(UpdateRestaurantCommand command, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantRepo.GetByIdAsync(command.id,cancellationToken);

            if (restaurant == null)
                return Result.Failure(RestaurantErrors.NotFound(command.id));

            restaurantRepo.Update(restaurant);

            var isPersisted= await unitOfWork.SaveChangesAsync(cancellationToken);

            if (!isPersisted)
                return Result.Failure(Error.Persistance);

            return Result.Success();
        }
    }
}
