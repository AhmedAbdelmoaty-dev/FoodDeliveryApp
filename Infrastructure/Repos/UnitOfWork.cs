using Application.Abstractions.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {


            return await _context.SaveChangesAsync(cancellationToken) > 0;
        } 

    }
}
