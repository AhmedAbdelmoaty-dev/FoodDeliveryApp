using Application.Common;
using Application.Features.Order.Dtos;
using Domain.Abstractions.Result;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Order.Queries.GetOrdersForRestaurant
{
    public record GetOrdersForRestaurantQuery(Guid RestaurantId,bool SortDesc=true,int PageIndex=1,int PageSize=10,OrderStatus status=OrderStatus.Placed)
        :IRequest<Result<PagedResponse<OrderDto>>>;

    public class GetOrdersForRestaurantQueryValidator : AbstractValidator<GetOrdersForRestaurantQuery>
    {
        public GetOrdersForRestaurantQueryValidator()
        {
            RuleFor(x => x.RestaurantId).NotEmpty().WithMessage("restaurant id is required");
        }
    }

}
