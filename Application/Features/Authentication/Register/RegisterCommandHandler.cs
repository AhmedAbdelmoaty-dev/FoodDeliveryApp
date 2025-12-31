using Application.Abstractions.Services;
using Application.Features.Authentication.Dtos;
using Domain.Abstractions.Result;
using Domain.Entities;
using Domain.Errors;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Authentication.Register
{
    internal class RegisterCommandHandler(UserManager<User> userManager, ITokenService tokenService)
        : IRequestHandler<RegisterCommand, Result<AuthDto>>
    {
        public async Task<Result<AuthDto>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var existedUser = await userManager.FindByEmailAsync(command.Email);

            if (existedUser is not null)
                return Result<AuthDto>.Failure(AuthErrors.AlreadyExist(command.Email));

            var user = command.Adapt<User>();

            var createResult = await userManager.CreateAsync(user, command.Password);

            if (!createResult.Succeeded)
                return Result<AuthDto>.Failure(AuthErrors.RegisterFailed(createResult.Errors.Select(e => e.Description).ToList()));

            //remmember to add roles in future
            var accessToken=  tokenService.CreateToken(user, new List<string>());

            var refreshToken = tokenService.CreateRefreshToken();

            user.RefreshTokens.Add(refreshToken);

           await userManager.UpdateAsync(user);

            //remmember to add roles in future
            var authDto = new AuthDto
            {
                Email = user.Email,
                UserName = user.UserName,
                UserId = user.Id,
                IsAuthenticated = true,
                Token = accessToken,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiryTime,
                Roles = new List<string>()
            };

            return Result<AuthDto>.Success(authDto);
        }
    }
}
