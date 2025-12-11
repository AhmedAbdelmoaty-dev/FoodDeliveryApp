using Application.Abstractions.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken=default) =>
           await _context.SaveChangesAsync() > 0;

    }
}
