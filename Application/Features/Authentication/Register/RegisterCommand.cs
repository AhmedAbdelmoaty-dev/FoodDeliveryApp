using Application.Features.Authentication.Dtos;
using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Authentication.Register
{
    public record RegisterCommand(string Email ,string UserName, string Password)
        :IRequest<Result<AuthDto>>;


    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(2).WithMessage("Username must be at least 3 characters long.");
           
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
        }
    }

}
