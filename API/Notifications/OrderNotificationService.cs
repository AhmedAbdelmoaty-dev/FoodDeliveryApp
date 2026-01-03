using API.Hubs;
using Application.Abstractions.Services;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace API.Notifications
{
    public class OrderNotificationService : IOrderNotificationService
    {
        private readonly IHubContext<OrdersHub,IOrderClient> _hubContext;

        public OrderNotificationService(IHubContext<OrdersHub, IOrderClient> hubContext)
        {
            _hubContext = hubContext;
        }
        public  async Task OrderCreatedAsync(Order order)
        {
           await _hubContext.Clients
                .Group($"Restaurant-{order.RestaurantId}")
                .OrderCreated(order.Id);
        }

        public async Task OrderStatusChangedAsync(Order order)
        {
            await _hubContext.Clients
                .Group($"User-{order.CustomerId}")
                .OrderStatusChanged(order.Id,order.Status.ToString());   
        }
    }
}
