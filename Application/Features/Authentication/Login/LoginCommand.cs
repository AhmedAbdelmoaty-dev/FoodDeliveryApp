using Application.Features.Authentication.Dtos;
using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Authentication.Login
{
    public record LoginCommand(string Email, string Password)
        :IRequest<Result<AuthDto>>;


    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }   

}
