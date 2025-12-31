using Application.Features.Order.Dtos;
using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Order.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderItemDto[] OrderItems, Guid RestaurantId,Guid UserId)
        :IRequest<Result<Guid>>;

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.RestaurantId)
                .NotEmpty().WithMessage("RestaurantId is required.");
           
            RuleFor(x => x.OrderItems)
                .NotNull().WithMessage("OrderItems is required.")
                .NotEmpty().WithMessage("At least one order item is required.");
           
            RuleForEach(x => x.OrderItems)
                .SetValidator(new OrderItemDtoValidator());

        }

        public class OrderItemDtoValidator: AbstractValidator<OrderItemDto>
        {
            public OrderItemDtoValidator()
            {
                RuleFor(x => x.MenuItemId)
                    .NotEmpty().WithMessage("MenuItemId is required.");
               
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
               
                RuleFor(x => x.UnitPrice)
                    .GreaterThan(0).WithMessage("UnitPrice must be greater than zero.");
              
                RuleFor(x => x.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
            }
        }
    }

}
