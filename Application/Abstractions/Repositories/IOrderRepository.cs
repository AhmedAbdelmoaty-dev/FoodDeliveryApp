using Application.Abstractions.Specification;
using Domain.Entities;

namespace Application.Abstractions.Repositories
{
    public interface IOrderRepository
    {
        Task<IReadOnlyList<Order>> GetOrdersBySpecAsync(Specification<Order> spec,CancellationToken cancellationToken=default);
        Task<Order?> GetByIdAsync(Guid id,CancellationToken cancellationToken=default);
        Task<int> CountAsync(Specification<Order> spec, CancellationToken cancellationToken=default);
        void Add(Order Entity);
    }
}
