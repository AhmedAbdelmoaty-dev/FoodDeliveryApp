using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Restaurants.Commands.DeleteMenuItem
{
    public record DeleteMenuItemCommand(Guid RestaurantId, Guid MenuItemId)
        : IRequest<Result>;

    public class DeleteMenuItemCommandValidator : AbstractValidator<DeleteMenuItemCommand>
    {
        public DeleteMenuItemCommandValidator()
        {
            RuleFor(x => x.RestaurantId)
                .NotEmpty();
            
            RuleFor(x => x.MenuItemId)
                .NotEmpty();
        }
    }

}
