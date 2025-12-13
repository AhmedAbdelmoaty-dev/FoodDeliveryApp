using Application.Abstractions.Repositories;
using Domain.Abstractions.Result;
using Domain.Entities;
using Domain.Errors;
using MediatR;

namespace Application.Features.Restaurants.Commands.AddTagToRestaurant
{
    internal class AddTagToRestaurantCommandHandler
        (IRestaurantRepository restaurantRepo,ITagRepository tagRepo,IUnitOfWork unitOfWork)
        : IRequestHandler<AddTagToRestaurantCommand, Result>
    {
        public async Task<Result> Handle(AddTagToRestaurantCommand command, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantRepo
                .GetRestaurantWithTagsAsync(command.RestaurantId, cancellationToken);

            if (restaurant is null)
                return Result.Failure(RestaurantErrors.NotFound(command.RestaurantId));

          var existingTag=   restaurant.RestaurantTags
                .FirstOrDefault(x => x.TagId == command.TagId)?.Tag;

            if (existingTag is not null)
                return Result.Failure(RestaurantErrors.TagAlreadyExist(existingTag.Name));

            var isExistInDb = await tagRepo.IsExsistsAsync(command.TagId, cancellationToken); 

            if(!isExistInDb)
                return Result.Failure(TagErrors.NotFound(command.TagId));

            var linkedRecord = new RestaurantTag
            {
                Id = Guid.NewGuid(),
                TagId=command.TagId,
                RestaurantId=command.RestaurantId
            };

            restaurant.RestaurantTags.Add(linkedRecord);

          var isPersisted=  await unitOfWork.SaveChangesAsync(cancellationToken);

            if(!isPersisted)
                return Result.Failure(Error.Persistance);

            return Result.Success();
        }
    }
}
