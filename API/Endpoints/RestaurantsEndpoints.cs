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

                result.ToHttpResult();
            });

            group.MapPost("", async (CreateRestaurantCommand command ,ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(command, ct);

                return result.ToHttpResult();
            });

            group.MapDelete("{id}", async (Guid id, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(new DeleteRestaurantCommand(id), ct);

                return result.ToHttpResult();
            });

            group.MapPatch("{id}", async (UpdateRestaurantCommand command, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(command, ct);

                return result.ToHttpResult();
            });

            group.MapPost("/{restaurantId}/Menu-items", async (CreateMenuItemCommand command, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(command, ct);
                
                return result.ToHttpResult();
            });

            group.MapPut("/{restaurantId}/Menu-items/{menuItemId}",async(UpdateMenuItemCommand command, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(command, ct);
                
                return result.ToHttpResult();
            });

            group.MapDelete("/{restaurantId}/Menu-items/{menuItemId}", async([AsParameters]DeleteMenuItemCommand command, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(command,ct);
                
                return result.ToHttpResult();
            });

            

        }
    }
}
