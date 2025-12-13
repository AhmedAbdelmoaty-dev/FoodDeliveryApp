using Domain.Entities;

namespace Application.Abstractions.Repositories
{
    public interface ITagRepository
    {
        void Rename(Tag tag);

        void Add(Tag tag);

        Task<IReadOnlyList<Tag?>> GetAllAsync(CancellationToken cancellationToken=default);

        Task<Tag> GetByIdAsync(Guid id,CancellationToken cancellationToken=default); 

        Task<bool> IsExsistsAsync(Guid id,CancellationToken cancellationToken=default);
    }
}
