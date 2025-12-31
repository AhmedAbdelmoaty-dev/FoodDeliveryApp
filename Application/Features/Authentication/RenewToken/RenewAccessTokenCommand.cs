using Application.Features.Authentication.Dtos;
using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Authentication.RenewToken
{
    public record RenewAccessTokenCommand(string RefreshToken) : IRequest<Result<AuthDto>>;

    public class RenewAccessTokenCommandValidator : AbstractValidator<RenewAccessTokenCommand>
    {
        public RenewAccessTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required.");
        }
    }

}
