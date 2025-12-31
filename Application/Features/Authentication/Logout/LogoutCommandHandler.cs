using Application.Abstractions.Services;
using Domain.Abstractions.Result;
using Domain.Entities;
using Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Authentication.Logout
{
    internal class LogoutCommandHandler(UserManager<User> userManager,ITokenService tokenService)
        : IRequestHandler<LogoutCommand, Result>
    {
        public async Task<Result> Handle(LogoutCommand command, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(command.email);

            if (user is null)
                return Result.Failure(AuthErrors.InvalidCredential);

            var result = await tokenService.RevokeRefreshTokenAsync(command.refreshToken);

            return result;  

        }
    }
}
