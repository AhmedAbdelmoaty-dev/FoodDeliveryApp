using Application.Abstractions.Repositories;
using Application.Common;
using Application.Specifications;
using Domain.Abstractions;
using Mapster;
using MediatR;

namespace Application.Restaurants.Queries.ListRestaurants
{
    internal class GetRestaurantsQueryHandler(IRestaurantRepository restaurantRepo)
        : IRequestHandler<GetRestaurantsQuery, Result<PagedResponse<RestaurantDto>>>
    {
        public async Task<Result<PagedResponse<RestaurantDto>>> Handle(GetRestaurantsQuery query, CancellationToken cancellationToken)
        {
            var spec = new RestaurantsPaginatedSpec(query.requestOrder, query.PageIndex, query.PageSize);
           
            var restaurants= await restaurantRepo.GetBySpecAsync(spec,cancellationToken);

            var TotalCount = await restaurantRepo.CountAsync(cancellationToken);

            var RestaurantDtos= restaurants.Adapt<IReadOnlyList<RestaurantDto>>(); 

            var pagedResponse= new PagedResponse<RestaurantDto>(query.PageIndex,query.PageSize,TotalCount,RestaurantDtos);

            return Result<PagedResponse<RestaurantDto>>.Success(pagedResponse);

            
        }
    }
}
