using Application.Abstractions.Specification;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery
            (IQueryable<TEntity> inputQuery, Specification<TEntity> spec, bool isCountQuery = false)
        {
            var query = inputQuery;

            foreach (var include in spec.Includes)
            {
                query = query.Include(include);
            }

            if(spec.Criteria is not null)
                query.Where(spec.Criteria);

            if (isCountQuery)
                return query;

            if(spec.OrderBy is not null)
                query.OrderBy(spec.OrderBy);
            
            if(spec.OrderByDesc is not null)
                query.OrderByDescending(spec.OrderByDesc);

            if(spec.IsPagingEnabled)
                query.Skip(spec.Skip).Take(spec.Take);

            return query;
        }
    }
}
