using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Domain.Abstractions.Result;
using Domain.Entities;
using Domain.Errors;
using MediatR;


namespace Application.Features.Order.Commands.MarkPreparing
{
    internal class MarkOrderPreparingCommandHandler(IOrderRepository orderRepo
        ,IUnitOfWork unitOfWork,IOrderNotificationService notificationService)
        : IRequestHandler<MarkOrderPreparingCommand, Result>
    {
        public async Task<Result> Handle(MarkOrderPreparingCommand command, CancellationToken cancellationToken)
        {
            var order = await orderRepo.GetByIdAsync(command.Id, cancellationToken);

            if (order is null)
                return Result.Failure(OrderErrors.NotFound(command.Id));

            order.Status = OrderStatus.Preparing;

           var isPersisted= await unitOfWork.SaveChangesAsync(cancellationToken);

            if (!isPersisted)
                return Result.Failure(Error.Persistance);

           await notificationService.OrderStatusChangedAsync(order);

           return Result.Success();
        }
    }
}
