using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Restaurants.Commands.CreateMenuItem
{
    public record CreateMenuItemCommand(Guid RestaurantId,string Name,string Description, string ImageUrl, decimal Price )
        :IRequest<Result<Guid>>;

    public class CreateMenuItemCommandValidator:AbstractValidator<CreateMenuItemCommand>
    {
        public CreateMenuItemCommandValidator()
        {
            RuleFor(x=>x.RestaurantId)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
            
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(200);
            
            RuleFor(x => x.Price)
                .GreaterThan(0);
        }
    }

}
