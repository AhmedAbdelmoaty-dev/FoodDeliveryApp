using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Restaurants.Queries.GetRestaurantWIthItems
{
    public record GetRestaurantWithItemQuery(Guid Id):IRequest<Result<RestaurantDto>>;

    public class GetRestaurantWithItemQueryValidator : AbstractValidator<GetRestaurantWithItemQuery>
    {
        public GetRestaurantWithItemQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Restaurant Id cannot be empty");
        }
    }

}
