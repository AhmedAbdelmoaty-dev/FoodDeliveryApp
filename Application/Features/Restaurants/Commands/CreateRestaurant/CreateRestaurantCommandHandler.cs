using Application.Abstractions.Repositories;
using Domain.Abstractions.Result;
using Domain.Entities;
using Domain.Errors;
using Mapster;
using MediatR;

namespace Application.Features.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(IUnitOfWork unitOfWork,IRestaurantRepository restaurantRepo)
        : IRequestHandler<CreateRestaurantCommand, Result<Guid>>
    {

        public async Task<Result<Guid>> Handle(CreateRestaurantCommand command, CancellationToken cancellationToken)
        {
            var restaurant = command.Adapt<Restaurant>();
            restaurant.Id = Guid.NewGuid();

            restaurantRepo.Create(restaurant);

            var isSuccess= await unitOfWork.SaveChangesAsync(cancellationToken);

            if (!isSuccess)
                return Result<Guid>.Failure(Error.Persistance);

            return Result<Guid>.Success(restaurant.Id);
        }
    }
}
