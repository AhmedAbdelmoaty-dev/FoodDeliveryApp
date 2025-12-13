using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Restaurants.Commands.AddTagToRestaurant
{
    public record AddTagToRestaurantCommand(Guid RestaurantId, Guid TagId)
        : IRequest<Result>;

    public class AddTagToRestaurantCommanValidator:AbstractValidator<AddTagToRestaurantCommand>
    {
        public AddTagToRestaurantCommanValidator()
        {
            RuleFor(x => x.RestaurantId)
                .NotEmpty();
            
            RuleFor(x => x.TagId)
                .NotEmpty();
        }
    }

}
