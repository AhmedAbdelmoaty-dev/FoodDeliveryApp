using Application.Common;
using Application.Features.Restaurants;
using Domain.Abstractions.Result;
using MediatR;

namespace Application.Features.Restaurants.Queries.ListRestaurants
{
    public record GetRestaurantsQuery(bool requestOrder=false,int PageSize=1,int PageIndex=10)
        :IRequest<Result<PagedResponse<RestaurantDto>>>;

}
