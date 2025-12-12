using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Restaurants.Commands.CreateRestaurant
{
    public record CreateRestaurantCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string LogoUrl { get; set; }
    }

    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(1, 20);

            RuleFor(x => x.LogoUrl).NotEmpty();
        }
    }



}
