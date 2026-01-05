using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Domain.Abstractions.Result;
using Domain.Entities;
using Domain.Errors;
using MediatR;

namespace Application.Features.Order.Commands.CancelOrder
{
    public class CancelOrderCommandHandler(IOrderRepository orderRepository
        ,IUnitOfWork unitOfWork,IOrderNotificationService notificationService)
        : IRequestHandler<CancelOrderCommand, Result>
    {

        public async Task<Result> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
        {

          var order=  await orderRepository.GetByIdAsync(command.OrderId,cancellationToken);

            if (order is null)
                return Result.Failure(OrderErrors.NotFound(command.OrderId));

            if (command.CustomerId != order.CustomerId)
                return Result.Failure(OrderErrors.IdentityNotMatch);
           
            if (order.Status > OrderStatus.Accepted)
                return Result.Failure(OrderErrors.InvalidCancellation(command.OrderId));

            order.Status = OrderStatus.Cancelled;

           var isPersisted=  await unitOfWork.SaveChangesAsync(cancellationToken);

            if (!isPersisted)
                return Result.Failure(Error.Persistance);

           await notificationService.OrderStatusChangedAsync(order);

            return Result.Success();
        }
    }
}
