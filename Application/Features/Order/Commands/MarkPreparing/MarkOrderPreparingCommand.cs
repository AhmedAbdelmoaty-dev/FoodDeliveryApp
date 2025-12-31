using Domain.Abstractions.Result;
using FluentValidation;
using FluentValidation.Validators;
using MediatR;

namespace Application.Features.Order.Commands.MarkPreparing
{
    public record MarkOrderPreparingCommand(Guid Id):IRequest<Result>;

    public class MarkOrderPreparingCommandValidator : AbstractValidator<MarkOrderPreparingCommand>
    {
        public MarkOrderPreparingCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Id cannot be empty");
        }
    }


}
