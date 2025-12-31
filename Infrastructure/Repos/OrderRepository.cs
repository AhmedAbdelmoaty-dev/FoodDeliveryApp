using Application.Abstractions.Repositories;
using Application.Abstractions.Specification;
using Application.Specifications;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class OrderRepository(AppDbContext context) : IOrderRepository
    {
        public void Add(Order Entity)
        {
            context.Add(Entity);
        }

        public async Task<int> CountAsync(Specification<Order> spec, CancellationToken cancellationToken = default)
        {
          return  await ApplySpecifications(spec,true).CountAsync(cancellationToken); 
        }

        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
           return await context.Orders.FirstOrDefaultAsync(x=>x.Id==id,cancellationToken);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersBySpecAsync(Specification<Order> spec, CancellationToken cancellationToken = default)
        {
           return await  ApplySpecifications(spec).AsNoTracking().ToListAsync(cancellationToken);
        }


        private IQueryable<Order> ApplySpecifications(Specification<Order> spec,bool IsCountQuery=false)
        {
            
            return SpecificationEvaluator<Order>.GetQuery(context.Orders, spec, IsCountQuery);
        }
    }
}
