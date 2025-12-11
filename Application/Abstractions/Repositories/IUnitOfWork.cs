namespace Application.Abstractions.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
