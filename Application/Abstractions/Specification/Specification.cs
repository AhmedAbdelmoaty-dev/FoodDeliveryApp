using Domain.Common;
using System.Linq.Expressions;
using System.Text;

namespace Application.Abstractions.Specification
{
    public abstract class Specification<TEntity> where TEntity : BaseEntity
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; protected set; }

        protected Specification(Expression<Func<TEntity, bool>>? criteria = null)
        {
            Criteria = criteria;
        }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public List<Expression<Func<TEntity, object>>> Includes { get; init; } = new();


        protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderByDescExpression)
            => OrderByDesc = orderByDescExpression;

        protected void AddOrderBy(Expression<Func<TEntity,object>>orderByAscExpression)
            =>OrderBy = orderByAscExpression;
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
            => Includes.Add(includeExpression);

        protected void ApplyPaging(int skip, int take,bool isPagingEnabled=true)
        {
            Take = take;
            Skip = skip;
            IsPagingEnabled = isPagingEnabled;
        }

       
    }
}
