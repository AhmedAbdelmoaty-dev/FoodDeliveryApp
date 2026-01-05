using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Order.Commands.CancelOrder
{
    public record CancelOrderCommand(Guid CustomerId,Guid OrderId):IRequest<Result>;

    public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand> 
    {
        public CancelOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("userId cannot be empty or null");

            RuleFor(x => x.OrderId)
                .NotEmpty()
                .WithMessage("userId cannot be empty or null");
        }

    }

}
