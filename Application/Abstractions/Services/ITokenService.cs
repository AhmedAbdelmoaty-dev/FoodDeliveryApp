using Application.Features.Authentication.Dtos;
using Domain.Abstractions.Result;
using Domain.Entities;

namespace Application.Abstractions.Services
{
    public interface ITokenService
    {
        string CreateToken(User user, IList<string> Roles);
        Task<Result<AuthDto>> RenewAccessTokenAsync(string refreshToken);
        RefreshToken CreateRefreshToken();

        Task<Result> RevokeRefreshTokenAsync(string token);
    }
}
