using Application.Abstractions.Repositories;
using Domain.Abstractions.Result;
using Domain.Errors;
using Mapster;
using MediatR;

namespace Application.Features.Tags.Commands.RenameTag
{
    internal class RenameTagCommandHandler(ITagRepository tagRepo,IUnitOfWork unitOfWork)
        : IRequestHandler<RenameTagCommand, Result>
    {
        public async Task<Result> Handle(RenameTagCommand command, CancellationToken cancellationToken)
        {
            var tag = await tagRepo.GetByIdAsync(command.Id,cancellationToken);

            if (tag == null)
                return Result.Failure(TagErrors.NotFound(command.Id));

            tag.Name=command.Name;

            tagRepo.Rename(tag);

           var isPersisted= await unitOfWork.SaveChangesAsync(cancellationToken);

            if (!isPersisted)
                return Result.Failure(Error.Persistance);

            return Result.Success();
        }
    }
}
