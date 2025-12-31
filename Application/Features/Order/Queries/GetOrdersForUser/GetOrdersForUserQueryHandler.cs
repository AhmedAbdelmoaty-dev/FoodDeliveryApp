using Application.Abstractions.Repositories;
using Application.Common;
using Application.Features.Order.Dtos;
using Application.Specifications;
using Domain.Abstractions.Result;
using Mapster;
using MediatR;

namespace Application.Features.Order.Queries.GetOrdersForUser
{
    public class GetOrdersForUserQueryHandler(IOrderRepository orderRepo)
        : IRequestHandler<GetOrdersForUserQuery, Result<PagedResponse<OrderDto>>>
    {
        public async Task<Result<PagedResponse<OrderDto>>> Handle(GetOrdersForUserQuery query, CancellationToken cancellationToken)
        {
            var spec = new OrdersForUserSpec(query.UserId, query.SortDesc, query.status, query.PageIndex, query.PageSize);

            var orders=  await orderRepo.GetOrdersBySpecAsync(spec, cancellationToken);

            var count = await orderRepo.CountAsync(spec, cancellationToken);

            var ordersDtos = orders.Adapt<IReadOnlyList<OrderDto>>();

            var pagedResponse = new PagedResponse<OrderDto>(query.PageIndex, query.PageSize, count, ordersDtos);

            return Result<PagedResponse<OrderDto>>.Success(pagedResponse);
        }
    }
}
