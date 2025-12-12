using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Restaurants.Commands.UpdateRestaurant
{
    public record UpdateRestaurantCommand(Guid id , string Name, string Address, string LogoUrl)
        :IRequest<Result>;


    public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(1, 20);

            RuleFor(x => x.LogoUrl).NotEmpty();
        }
    }

}
