using Application.Common;
using Application.Features.Order.Dtos;
using Domain.Abstractions.Result;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Order.Queries.GetOrdersForUser
{
     public record GetOrdersForUserQuery(Guid UserId, bool SortDesc = true, int PageIndex = 1, int PageSize = 10, OrderStatus status = OrderStatus.Placed)
        :IRequest<Result<PagedResponse<OrderDto>>>;


    public class GetOrdersForUserQueryValidator : AbstractValidator<GetOrdersForUserQuery>
    {
        public GetOrdersForUserQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("user id is required");
        }
    }


}
