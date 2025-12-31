using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Authentication.Logout
{
    public record LogoutCommand(string email,string refreshToken):IRequest<Result>;


    public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidator()
        {
            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }

}
