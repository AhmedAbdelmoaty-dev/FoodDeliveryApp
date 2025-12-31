
using Application.Abstractions.Services;
using Application.Features.Authentication.Dtos;
using Domain.Abstractions.Result;
using Domain.Entities;
using Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Application.Features.Authentication.Login
{
    internal class LoginCommandHandler(UserManager<User> userManager,ITokenService tokenService)
        : IRequestHandler<LoginCommand, Result<AuthDto>>
    {
        public async Task<Result<AuthDto>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(command.Email);

            if (user is null )
                return Result<AuthDto>.Failure(AuthErrors.InvalidCredential);


            var isCorrectPassword= await userManager.CheckPasswordAsync(user, command.Password);

            if (!isCorrectPassword)
                return Result<AuthDto>.Failure(AuthErrors.InvalidCredential);

            // add roles in future
         //   var roles = await userManager.GetRolesAsync(user);

            var accessToken = tokenService.CreateToken(user, new List<string>());

            var refreshToken = tokenService.CreateRefreshToken();

            user.RefreshTokens.Add(refreshToken);

            await userManager.UpdateAsync(user);

            var authDto = new AuthDto
            {
                Email = user.Email,
                UserName = user.UserName,
                UserId = user.Id,
                IsAuthenticated = true,
                Token = accessToken,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiryTime,
               
                Roles = new List<string>() //add roles in future
            };

            return Result<AuthDto>.Success(authDto);    

        }
    }
}
