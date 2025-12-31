using Application.Abstractions.Services;
using Application.Features.Authentication.Dtos;
using Domain.Abstractions.Result;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Application.Features.Authentication.RenewToken
{
    internal class RenewAccessTokenCommandHandler(ITokenService tokenService,UserManager<User> userManager)
        : IRequestHandler<RenewAccessTokenCommand, Result<AuthDto>>
    {
        public Task<Result<AuthDto>> Handle(RenewAccessTokenCommand command, CancellationToken cancellationToken)
        {
            var result= tokenService.RenewAccessTokenAsync(command.RefreshToken);

            return result;
        }
    }
}
