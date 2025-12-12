using Application.Abstractions.Repositories;
using Domain.Abstractions.Result;
using Domain.Errors;
using Mapster;
using MediatR;

namespace Application.Features.Tags.Queries.GetById
{
    internal class GetTagByIdQueryHandler(ITagRepository tagRepo)
        : IRequestHandler<GetTagByIdQuery, Result<TagDto>>
    {
        public async Task<Result<TagDto>> Handle(GetTagByIdQuery query, CancellationToken cancellationToken)
        {
            var tag = await tagRepo.GetByIdAsync(query.Id,cancellationToken);

            if (tag == null)
                return Result<TagDto>.Failure(TagErrors.NotFound(query.Id));

            return Result<TagDto>.Success(tag.Adapt<TagDto>());
        }
    }
}
