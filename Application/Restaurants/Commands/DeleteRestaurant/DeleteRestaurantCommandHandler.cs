using Application.Abstractions.Repositories;
using Domain.Abstractions;
using Domain.Errors;
using MediatR;

namespace Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(IUnitOfWork unitOfWork,IRestaurantRepository restaurantRepo)
        : IRequestHandler<DeleteRestaurantCommand, Result>
    {
        public async Task<Result> Handle(DeleteRestaurantCommand command, CancellationToken cancellationToken)
        {
          var restaurant = await  restaurantRepo.GetByIdAsync(command.id,cancellationToken);

            if (restaurant is null)
                return Result.Failure(RestaurantErrors.NotFound(command.id));

            restaurantRepo.Delete(restaurant);

           var isSuccess= await unitOfWork.SaveChangesAsync(cancellationToken);

            if (!isSuccess)
                return Result.Failure(Error.Persistance);

            return Result.Success();
        }
    }
}
