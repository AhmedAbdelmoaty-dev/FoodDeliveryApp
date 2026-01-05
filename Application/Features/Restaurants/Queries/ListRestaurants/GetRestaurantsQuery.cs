using Application.Abstractions.Caching;
using Application.Common;
using Domain.Abstractions.Result;
using MediatR;

namespace Application.Features.Restaurants.Queries.ListRestaurants
{
    public record GetRestaurantsQuery(bool requestOrder = false, int PageSize = 10, int PageIndex = 1)
        : IRequest<Result<PagedResponse<RestaurantDto>>>;


}
