using Application.Abstractions.Repositories;
using Domain.Abstractions.Result;
using Domain.Entities;
using Domain.Errors;
using Mapster;
using MediatR;

namespace Application.Features.Tags.Commands.CreateTag
{
    internal class CreateTagCommandHandler(ITagRepository tagRepository,IUnitOfWork unitOfWork)
        : IRequestHandler<CreateTagCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateTagCommand command, CancellationToken cancellationToken)
        {
            var tag =  command.Adapt<Tag>();
           
            tag.Id = Guid.NewGuid();

            tagRepository.Add(tag);

            var isPersisted=  await unitOfWork.SaveChangesAsync(cancellationToken);

            if (!isPersisted)
                return Result<Guid>.Failure(Error.Persistance);

            return Result<Guid>.Success(tag.Id);
        }
    }
}
