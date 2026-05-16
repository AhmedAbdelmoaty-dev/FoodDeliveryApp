using API.Extensions;
using Application.Common;
using Application.Features.Restaurants;
using Application.Features.Restaurants.Commands.CreateRestaurant;
using Application.Features.Restaurants.Commands.UpdateRestaurant;
using Application.Features.Restaurants.Queries.ListRestaurants;
using Application.Features.Restaurants.Commands.DeleteRestaurant;
using Application.Features.Restaurants.Queries.GetRestaurant;
using MediatR;
using Application.Features.Restaurants.Commands.CreateMenuItem;
using Application.Features.Restaurants.Commands.UpdateMenuItem;
using Application.Features.Restaurants.Commands.DeleteMenuItem;
using Application.Features.Restaurants.Queries.GetRestaurantWIthItems;
using Application.Features.Restaurants.Commands.UploadLogo;
using Application.Features.Restaurants.Commands.DeleteLogo;
using Application.Features.Restaurants.Commands.UploadMenuItemImage;
using Application.Features.Restaurants.Commands.DeleteMenuItemImage;
using Microsoft.AspNetCore.Authorization;

namespace API.Endpoints
{
    public static class RestaurantsEndpoints
    {
        public static void MapRestaurantsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/restaurants").WithTags("Restaurants");

            group.MapGet("/{id}", async (Guid id , ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(new GetRestaurantByIdQuery(id), ct);

               return result.ToHttpResult<RestaurantDto>();
            });


            group.MapGet("", async ([AsParameters] GetRestaurantsQuery query, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(query);

                return result.ToHttpResult<PagedResponse<RestaurantDto>>();
            });

            group.MapGet("/{id}/menu", async (Guid id, ISender sender) =>
            {

                var result = await sender.Send(new GetRestaurantWithItemQuery(id));

                return result.ToHttpResult<RestaurantDto>();
            });

            group.MapPost("", [Authorize] async (CreateRestaurantCommand command ,ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(command, ct);

                return result.ToHttpResult();
            });

            group.MapDelete("{id}", [Authorize] async (Guid id, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(new DeleteRestaurantCommand(id), ct);

                return result.ToHttpResult();
            });

            group.MapPatch("{id}", [Authorize] async (UpdateRestaurantCommand command, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(command, ct);

                return result.ToHttpResult();
            });

            group.MapPost("/{restaurantId}/Menu-items", [Authorize] async (CreateMenuItemCommand command, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(command, ct);
                
                return result.ToHttpResult();
            });

            group.MapPut("/{restaurantId}/Menu-items/{menuItemId}", [Authorize] async (UpdateMenuItemCommand command, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(command, ct);
                
                return result.ToHttpResult();
            });

            group.MapDelete("/{restaurantId}/Menu-items/{menuItemId}", [Authorize] async ([AsParameters]DeleteMenuItemCommand command, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(command,ct);
                
                return result.ToHttpResult();
            });

            group.MapPost("/{id}/logo", [Authorize] async (Guid id, IFormFile file, ISender sender, CancellationToken ct) =>
            {
                if (file.Length == 0)
                    return Results.BadRequest("File is required");

                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream, ct);
                var imageData = memoryStream.ToArray();

                var command = new UploadLogoCommand(id, imageData, file.FileName, file.ContentType);
                var result = await sender.Send(command, ct);

                return result.ToHttpResult<string>();
            });

            group.MapDelete("/{id}/logo", [Authorize] async (Guid id, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(new DeleteLogoCommand(id), ct);

                return result.ToHttpResult();
            });

            group.MapPost("/{restaurantId}/Menu-items/{menuItemId}/image", [Authorize] async (Guid restaurantId, Guid menuItemId, IFormFile file, ISender sender, CancellationToken ct) =>
            {
                if (file.Length == 0)
                    return Results.BadRequest("File is required");

                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream, ct);
                var imageData = memoryStream.ToArray();

                var command = new UploadMenuItemImageCommand(restaurantId, menuItemId, imageData, file.FileName, file.ContentType);
                var result = await sender.Send(command, ct);

                return result.ToHttpResult<string>();
            });

            group.MapDelete("/{restaurantId}/Menu-items/{menuItemId}/image", [Authorize] async (Guid restaurantId, Guid menuItemId, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(new DeleteMenuItemImageCommand(restaurantId, menuItemId), ct);

                return result.ToHttpResult();
            });

        }
    }
}
