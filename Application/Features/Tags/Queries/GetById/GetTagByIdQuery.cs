using Domain.Abstractions.Result;
using MediatR;

namespace Application.Features.Tags.Queries.GetById
{
    public record GetTagByIdQuery(Guid Id):IRequest<Result<TagDto>>;
    
}
