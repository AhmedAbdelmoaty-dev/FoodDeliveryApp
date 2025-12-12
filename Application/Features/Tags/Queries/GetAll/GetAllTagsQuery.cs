
using Domain.Abstractions.Result;
using MediatR;

namespace Application.Features.Tags.Queries.GetAll
{
    public record GetAllTagsQuery:IRequest<Result<IReadOnlyList<TagDto>>>;

}
