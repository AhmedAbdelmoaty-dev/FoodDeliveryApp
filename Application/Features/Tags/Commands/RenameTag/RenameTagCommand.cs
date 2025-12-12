using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Tags.Commands.RenameTag
{
    public record RenameTagCommand(Guid Id,string Name):IRequest<Result>;


    public class RenameTagCommandValidator : AbstractValidator<RenameTagCommand>
    {
        public RenameTagCommandValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().Length(1,20);
        }
    }

}
