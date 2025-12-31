using Domain.Entities;

namespace Application.Features.Order.Dtos
{
    public class OrderItemDto
    {
        public Guid MenuItemId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }

       public OrderItem ToOrderItem()
        {
          return new OrderItem
            {
                MenuItemId = this.MenuItemId,
                Name = this.Name,
                UnitPrice = this.UnitPrice,
                Quantity = this.Quantity
               
            };
        }
    }
}
