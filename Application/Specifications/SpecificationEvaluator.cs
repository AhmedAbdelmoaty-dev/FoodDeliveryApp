using Application.Abstractions.Specification;
using Domain.Common;

namespace Application.Specifications
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery 
            (IQueryable<TEntity> inputQuery, Specification<TEntity> spec)
        {
            var query = inputQuery;

            if(spec.Criteria is not null)
                query.Where(spec.Criteria);

            if(spec.OrderByDesc is not null)
                query.OrderByDescending(spec.OrderByDesc);

            if(spec.IsPagingEnabled)
                query.Skip(spec.Skip).Take(spec.Take);

            return query;
        }
    }
}
