using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Order.Commands.AcceptOrder
{
    public record AcceptOrderCommand(Guid Id):IRequest<Result>;

    public class AcceptOrderCommandValidator:AbstractValidator<AcceptOrderCommand>
    {
        public AcceptOrderCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
        }
    }

}
