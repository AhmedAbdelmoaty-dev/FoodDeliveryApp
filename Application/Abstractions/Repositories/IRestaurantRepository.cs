
using Application.Abstractions.Specification;
using Domain.Entities;

namespace Application.Abstractions.Repositories
{
    public interface IRestaurantRepository
    {
        Task<IReadOnlyList<Restaurant>> GetBySpecAsync(Specification<Restaurant> spec , CancellationToken cancellationToken=default);

        Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<int> CountAsync(CancellationToken cancellationToken = default);

        void Create(Restaurant entity);

        void Delete( Restaurant entity );

        void Update(Restaurant enttiy );


    }
}
