using Domain.Common;

namespace Domain.Entities
{
    public class OrderItem:BaseEntity
    {
        public Guid MenuItemId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalPrice=>UnitPrice*Quantity;
    }
}
