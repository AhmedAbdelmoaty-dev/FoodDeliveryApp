using Application.Features.Order.Dtos;
using Domain.Abstractions.Result;
using FluentValidation;
using MediatR;

namespace Application.Features.Order.Queries.GetById
{
    public record GetOrderByIdQuery(Guid Id)
        : IRequest<Result<OrderDto>>;

    public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
    {
        public GetOrderByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Order Id is required.");
        }
    }   



}
