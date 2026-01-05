using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace API.Hubs
{
    public class OrdersHub:Hub<IOrderClient>
    {
        public override async Task OnConnectedAsync()
        {
            var user = Context.User;

            if (user?.Identity?.IsAuthenticated != true)
            {
                Context.Abort();
                return;
            }
            // make sure you add right claims to the token when you make the frontend part
            // if its a customer
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
                await Groups.AddToGroupAsync(Context.ConnectionId, $"User-{userId}");

            // if its a restaurant admin
            var restaurantId = user.FindFirst("RestaurantId")?.Value;

            if (!string.IsNullOrEmpty(restaurantId))
             await   Groups.AddToGroupAsync(Context.ConnectionId, $"Restaurant-{restaurantId}");

           

            await base.OnConnectedAsync();
        }
    
    }


    public interface IOrderClient
    {
        Task OrderCreated(Guid orderId);

        Task OrderStatusChanged(Guid orderId,string status);
    }
}
