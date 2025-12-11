using Application.Abstractions.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class RestaurantsPaginatedSpec:Specification<Restaurant>
    {
        public RestaurantsPaginatedSpec(bool requestOrder, int pageNumber=1,int pageSize=10)
        {
            // the order based staticaly on name will be modified when adding reviews feature
            if (requestOrder)
            AddOrderByDesc(x=>x.Name);

            var skip = (pageNumber-1)* pageSize;

            ApplyPaging(skip, pageSize);
        }
    }
}
