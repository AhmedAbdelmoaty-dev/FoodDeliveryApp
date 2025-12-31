using Domain.Common;

namespace Domain.Entities
{
    public class Order:BaseEntity
    {
        public Guid CustomerId { get; set; }
        public Guid RestaurantId { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
        public decimal OrderPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.UtcNow;

    }
}
