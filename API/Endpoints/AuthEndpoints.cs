using API.Extensions;
using Application.Features.Authentication.Dtos;
using Application.Features.Authentication.Login;
using Application.Features.Authentication.Logout;
using Application.Features.Authentication.Register;
using Application.Features.Authentication.RenewToken;
using Domain.Abstractions.Result;
using Domain.Errors;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace API.Endpoints
{

    public record RefreshTokenRequest(string RefreshToken);
    public static class AuthEndpoints
    {

        public static void MapAuthEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/auth").WithTags("Authentication");

            group.MapPost("/login", async (LoginCommand command, ISender sender,HttpContext context,CancellationToken ct) =>
            {
                var result = await sender.Send(command,ct);

                if (result.IsSuccess)
                    AddTokenToCookie(context, result.Value.RefreshToken, result.Value.RefreshTokenExpiration);

                return result.ToHttpResult<AuthDto>();
            });

            group.MapPost("/register", async (RegisterCommand command, ISender sender,HttpContext context, CancellationToken ct) =>
            {
                var result = await sender.Send(command,ct);

                if (result.IsSuccess)
                    AddTokenToCookie(context, result.Value.RefreshToken, result.Value.RefreshTokenExpiration);

                return result.ToHttpResult<AuthDto>();
            });

            group.MapPost("/logout", async (LogoutCommand command, ISender sender,HttpContext context, CancellationToken ct) =>
            {
                var result = await sender.Send(command,ct);

                if (result.IsSuccess)
                    context.Response.Cookies.Delete("refresh-token");


                return result.ToHttpResult();
            }); 

            group.MapPost("/renew-token", async (HttpContext context ,ISender sender, CancellationToken ct) =>
            {
                var refreshToken = context.Request.Cookies["refresh-token"];

                var result = await sender.Send(new RenewAccessTokenCommand(refreshToken),ct);

                if (result.IsSuccess)
                    AddTokenToCookie(context, result.Value.RefreshToken, result.Value.RefreshTokenExpiration);

                return result.ToHttpResult<AuthDto>();
            });

            group.MapGet("/secure", () =>
            {
                return Results.Ok("You are authenticated");
            }).RequireAuthorization();



        }
        private static void AddTokenToCookie(HttpContext context,string refreshToken,DateTime expiry)
        {
            context.Response.Cookies.Append("refresh-token", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = expiry
            });
        }
        
    }
}
