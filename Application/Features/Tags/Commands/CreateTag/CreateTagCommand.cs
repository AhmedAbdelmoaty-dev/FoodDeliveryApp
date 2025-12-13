using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Tags.Commands.CreateTag
{
    public record CreateTagCommand(string Name):IRequest<Result<Guid>>;

    public class CreateTagCommandValidator: AbstractValidator<CreateTagCommand>
    {
        public CreateTagCommandValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().Length(1,20);
        }
    }


}
