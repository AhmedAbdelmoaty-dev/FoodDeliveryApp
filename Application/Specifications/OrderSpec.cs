using Application.Abstractions.Specification;
using Domain.Entities;


namespace Application.Specifications
{
    public class OrdersForRestaurantSpec : Specification<Order>
    {
        public OrdersForRestaurantSpec(Guid restaurantId,bool sortDesc,OrderStatus status,int pageNumber = 1, int pageSize = 10):base(x => x.Status == status && x.RestaurantId == restaurantId)
        {
            if (sortDesc)
                AddOrderByDesc(x => x.CreatedAt);
            else
                AddOrderBy(x=>x.CreatedAt);

            var skip = (pageNumber - 1) * pageSize;

            ApplyPaging(skip, pageSize);

        }
    }

    public class OrdersForUserSpec : Specification<Order>
    {
        public OrdersForUserSpec(Guid userId, bool sortDesc, OrderStatus status = OrderStatus.Delivered, int pageNumber = 1, int pageSize = 10)
            :base(x=>x.CustomerId==userId&&x.Status==status)
        {
            if (sortDesc)
                AddOrderByDesc(x => x.CreatedAt);

            var skip = (pageNumber - 1) * pageSize;

            ApplyPaging(skip, pageSize);
        }
    }
}
