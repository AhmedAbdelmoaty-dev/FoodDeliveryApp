using Application.Abstractions.Repositories;
using Domain.Abstractions.Result;
using Domain.Entities;
using Domain.Errors;
using MediatR;


namespace Application.Features.Order.Commands.MarkPreparing
{
    internal class MarkOrderPreparingCommandHandler(IOrderRepository orderRepo,IUnitOfWork unitOfWork)
        : IRequestHandler<MarkOrderPreparingCommand, Result>
    {
        public async Task<Result> Handle(MarkOrderPreparingCommand command, CancellationToken cancellationToken)
        {
            var order = await orderRepo.GetByIdAsync(command.Id, cancellationToken);

            if (order is null)
                return Result.Failure(OrderErrors.NotFound(command.Id));

            order.Status = OrderStatus.Preparing;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
