using Application.Abstractions.Repositories;
using Application.Features.Order.Dtos;
using Domain.Abstractions.Result;
using Domain.Errors;
using Mapster;
using MediatR;

namespace Application.Features.Order.Queries.GetById
{
    public class GetOrderByIdQueryHandler(IOrderRepository orderRepo)
        : IRequestHandler<GetOrderByIdQuery, Result<OrderDto>>
    {
        public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
          var order= await orderRepo.GetByIdAsync(query.Id,cancellationToken);

            if (order is null)
                return Result<OrderDto>.Failure(OrderErrors.NotFound(query.Id));

            var MenuItemDtos = order.OrderItems.Select(x => x.Adapt<OrderItemDto>());

            var orderDto = new OrderDto
            {
                OrderItems = MenuItemDtos,
                Status = order.Status,
                OrderPrice = order.OrderPrice
            };
            return Result<OrderDto>.Success(orderDto);
        }
    }
}
