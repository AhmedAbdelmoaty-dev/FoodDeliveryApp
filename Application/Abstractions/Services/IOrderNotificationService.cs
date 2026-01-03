using Domain.Entities;

namespace Application.Abstractions.Services
{
    public interface IOrderNotificationService
    {
        Task OrderCreatedAsync(Order order);    
        Task OrderStatusChangedAsync(Order order);
    }
}
