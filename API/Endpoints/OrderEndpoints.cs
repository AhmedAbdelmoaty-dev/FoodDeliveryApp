using API.Extensions;
using Application.Features.Order.Commands.AcceptOrder;
using Application.Features.Order.Commands.CancelOrder;
using Application.Features.Order.Commands.CreateOrder;
using Application.Features.Order.Commands.MarkPreparing;
using Application.Features.Order.Queries.GetById;
using Application.Features.Order.Queries.GetOrdersForRestaurant;
using Application.Features.Order.Queries.GetOrdersForUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace API.Endpoints
{
    public static class OrderEndpoints
    {
        public static void MapOrderEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/orders").WithTags("Orders");
            
            group.MapPost("", [Authorize] async (CreateOrderCommand command, ISender sender, ClaimsPrincipal user, CancellationToken ct) =>
            {
                var userId = Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
                var orderWithUserId = new CreateOrderCommand(command.OrderItems, command.RestaurantId, userId);
                var result = await sender.Send(orderWithUserId, ct);
               
                return result.ToHttpResult();
            });

            group.MapGet("/{id:guid}", async (Guid id, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(new GetOrderByIdQuery(id), ct);
               
                return result.ToHttpResult();
            });

            group.MapGet("/restaurant", [Authorize] async ([AsParameters] GetOrdersForRestaurantQuery query,ISender sender) =>
            {
               var result= await sender.Send(query);

                return result.ToHttpResult();
            });
            group.MapGet("/user", [Authorize] async ([AsParameters] GetOrdersForUserQuery query, ISender sender) =>
            {
                var result = await sender.Send(query);

                return result.ToHttpResult();
            });

            group.MapGet("/accept/{id}", [Authorize] async (Guid id,ISender sender) =>
            {
                var result = await sender.Send(new AcceptOrderCommand(id));

                return result.ToHttpResult();
            });

            group.MapGet("/mark-preparing/{id}", [Authorize] async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new MarkOrderPreparingCommand(id));

                return result.ToHttpResult();
            });

            group.MapPost("/cancel/{id}", [Authorize] async (Guid id, ISender sender, ClaimsPrincipal user) =>
            {
                var userId = Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

                var result = await sender.Send(new CancelOrderCommand(userId, id));

                return result.ToHttpResult();
            });
        }
    }
}
