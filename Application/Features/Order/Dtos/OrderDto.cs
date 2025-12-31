using Domain.Entities;

namespace Application.Features.Order.Dtos
{
    public class OrderDto
    {
        public IEnumerable<OrderItemDto> OrderItems { get; set; } 
        public decimal OrderPrice { get; set; }
        public OrderStatus Status { get; set; }
    }
}
