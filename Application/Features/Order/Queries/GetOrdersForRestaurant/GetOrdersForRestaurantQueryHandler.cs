using Application.Abstractions.Repositories;
using Application.Common;
using Application.Features.Order.Dtos;
using Application.Specifications;
using Domain.Abstractions.Result;
using Mapster;
using MediatR;


namespace Application.Features.Order.Queries.GetOrdersForRestaurant
{
    public class GetOrdersForRestaurantQueryHandler(IOrderRepository orderRepo)
        : IRequestHandler<GetOrdersForRestaurantQuery, Result<PagedResponse<OrderDto>>>
    {
        public async Task<Result<PagedResponse<OrderDto>>> Handle(GetOrdersForRestaurantQuery query, CancellationToken cancellationToken)
        {
            var orderSpec = new OrdersForRestaurantSpec(query.RestaurantId,query.SortDesc,query.status,query.PageIndex,query.PageSize);

            var orders= await orderRepo.GetOrdersBySpecAsync(orderSpec, cancellationToken);

            var count = await orderRepo.CountAsync(orderSpec, cancellationToken);

            var ordersDtos = orders.Adapt<IReadOnlyList<OrderDto>>();

            var pagedResponse = new PagedResponse<OrderDto>(query.PageIndex, query.PageSize, count, ordersDtos);

            return Result<PagedResponse<OrderDto>>.Success(pagedResponse);

        }
    }
}
