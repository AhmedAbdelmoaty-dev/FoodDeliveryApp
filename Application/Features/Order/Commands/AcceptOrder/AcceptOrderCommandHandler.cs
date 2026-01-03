using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Domain.Abstractions.Result;
using Domain.Entities;
using Domain.Errors;
using MediatR;

namespace Application.Features.Order.Commands.AcceptOrder
{
    public class AcceptOrderCommandHandler(IOrderRepository orderRepo
        ,IUnitOfWork unitOfWork,IOrderNotificationService notificationService)
        : IRequestHandler<AcceptOrderCommand, Result>
    {
        public async Task<Result> Handle(AcceptOrderCommand command, CancellationToken cancellationToken)
        {
          var order=await orderRepo.GetByIdAsync(command.Id, cancellationToken);

            if (order is null)
                return Result.Failure(OrderErrors.NotFound(command.Id));

            order.Status = OrderStatus.Accepted;

           var isPersisted= await  unitOfWork.SaveChangesAsync(cancellationToken);

            if (!isPersisted)
                return Result.Failure(Error.Persistance);

            await notificationService.OrderStatusChangedAsync(order);

            return Result.Success();
        }
    }
}
