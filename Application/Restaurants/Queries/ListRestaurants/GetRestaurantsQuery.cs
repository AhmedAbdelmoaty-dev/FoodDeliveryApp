using Application.Common;
using Domain.Abstractions;
using MediatR;

namespace Application.Restaurants.Queries.ListRestaurants
{
    public record GetRestaurantsQuery(bool requestOrder=false,int PageSize=1,int PageIndex=10)
        :IRequest<Result<PagedResponse<RestaurantDto>>>;

}
