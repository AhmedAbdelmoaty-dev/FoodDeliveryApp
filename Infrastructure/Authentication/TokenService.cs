using Application.Abstractions.Services;
using Application.Features.Authentication.Dtos;
using Domain.Abstractions.Result;
using Domain.Entities;
using Domain.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly IOptions<JwtOptions> _jwtOptions;
        public TokenService(UserManager<User> userManager, IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions;

        }


        public string CreateToken(User user, IList<string> Roles)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var role in Roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenConfiguration = new JwtSecurityToken
            (
                issuer: _jwtOptions.Value.Issuer,
                audience: _jwtOptions.Value.Audience,
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes((double)(_jwtOptions.Value.DurationInMinutes)),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenConfiguration);

            return token;
        }


        public RefreshToken CreateRefreshToken()
        {

            var key = RandomNumberGenerator.GetBytes(64);

            var refreshToken = Convert.ToBase64String(key);

            return new RefreshToken
            {
                Token = refreshToken,
                ExpiryTime = DateTime.UtcNow.AddDays(10),
                CreatedAt = DateTime.UtcNow
            };
        }
        public async Task<Result<AuthDto>> RenewAccessTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.RefreshTokens.Any(x => x.Token == refreshToken));
            
            if (user == null)
              return  Result<AuthDto>.Failure(AuthErrors.InvalidRefreshToken);

            var oldRefreshToken = user.RefreshTokens.FirstOrDefault(x => x.Token == refreshToken);

            if (!oldRefreshToken.IsActive)
              return  Result<AuthDto>.Failure(AuthErrors.InvalidRefreshToken);


            oldRefreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = CreateRefreshToken();

            user.RefreshTokens.Add(newRefreshToken);

            var result = await _userManager.UpdateAsync(user);

            //add roles in future

            //var roles = await _userManager.GetRolesAsync(user);

            var accessToken = CreateToken(user, new List<string>());

            var authDto = new AuthDto
            {
                Email = user.Email,
                UserName = user.UserName,
                UserId = user.Id,
                IsAuthenticated = true,
                Token = accessToken,
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiration = newRefreshToken.ExpiryTime,
                Roles = new List<string>() // add roles in future
            }; 

            return Result<AuthDto>.Success(authDto);
        }

        public async Task<Result> RevokeRefreshTokenAsync(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
                return Result.Failure(AuthErrors.InvalidRefreshToken);

            var refreshToken = user.RefreshTokens.First(c => c.Token == token);

            if (!refreshToken.IsActive)
                return Result.Failure(AuthErrors.InvalidRefreshToken);

            refreshToken.RevokedOn = DateTime.UtcNow;

            var isPersisted = await _userManager.UpdateAsync(user);


            if (!isPersisted.Succeeded)
                return Result.Failure(Error.Persistance);


            return Result.Success();
        }

    }
}
