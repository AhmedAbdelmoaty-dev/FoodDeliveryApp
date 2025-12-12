using Application.Abstractions.Repositories;
using Domain.Abstractions.Result;
using Mapster;
using MediatR;

namespace Application.Features.Tags.Queries.GetAll
{
    internal class GetAllTagsQueryHandler(ITagRepository tagRepo) 
        : IRequestHandler<GetAllTagsQuery, Result<IReadOnlyList<TagDto>>>
    {
        public async Task<Result<IReadOnlyList<TagDto>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await tagRepo.GetAllAsync(cancellationToken);

            var tagsDtos = tags.Adapt<IReadOnlyList<TagDto>>();

            return Result<IReadOnlyList<TagDto>>.Success(tagsDtos);
        }
    }
}
