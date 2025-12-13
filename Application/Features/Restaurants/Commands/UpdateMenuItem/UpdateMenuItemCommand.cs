using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Restaurants.Commands.UpdateMenuItem
{
    public record UpdateMenuItemCommand(
        Guid RestaurantId,
        Guid MenuItemId,
        string Name,
        string Description,
        string ImageUrl,
        decimal Price
        ) : IRequest<Result>;

    public class UpdateMenuItemCommandValidator : AbstractValidator<UpdateMenuItemCommand>
    {
        public UpdateMenuItemCommandValidator()
        {
            RuleFor(x => x.RestaurantId)
                .NotEmpty();
           
            RuleFor(x=>x.MenuItemId)
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
