using Application.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class TagRepository(AppDbContext context) : ITagRepository
    {
        public void Add(Tag tag)
        {
            context.Tags.Add(tag);
        }

        public async Task<IReadOnlyList<Tag>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Tags.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Tag?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
           return await  context.Tags.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public void Rename(Tag tag)
        {
            context.Tags.Update(tag);
        }
    }
}
