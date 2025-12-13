using Application.Abstractions.Repositories;
using Domain.Abstractions.Result;
using Domain.Entities;
using Domain.Errors;
using Mapster;
using MediatR;

namespace Application.Features.Restaurants.Commands.CreateMenuItem
{
    internal class CreateItemCommandHandler(IRestaurantRepository restaurantRepository
         , IUnitOfWork unitOfWork)
        : IRequestHandler<CreateMenuItemCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateMenuItemCommand command, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantRepository
                .GetByIdAsync(command.RestaurantId, cancellationToken);

            if (restaurant is null)
                return Result<Guid>.Failure(RestaurantErrors.NotFound(command.RestaurantId));

            var menuItem = command.Adapt<MenuItem>();

            menuItem.Id = Guid.NewGuid();

            restaurant.MenuItems.Add(menuItem);

           var isPersisted= await  unitOfWork.SaveChangesAsync(cancellationToken);

            if (!isPersisted)
                return Result<Guid>.Failure(Error.Persistance);
            
            
            return Result<Guid>.Success(menuItem.Id);

        }
    }
}
