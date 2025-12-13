using Application.Abstractions.Repositories;
using Application.Abstractions.Specification;
using Application.Specifications;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class RestaurantRepository (AppDbContext context) : IRestaurantRepository
    {
      

        public async Task<IReadOnlyList<Restaurant>> GetBySpecAsync(Specification<Restaurant> spec,
            CancellationToken cancellationToken = default)
        {
         
          return  await ApplySpecifications(spec).AsNoTracking()
                .ToListAsync(cancellationToken);

        }

        public async Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
           return await context.Restaurants
                .FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
        }

        public async Task<Restaurant?> GetRestaurantWithListMenuItemsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Restaurants
                .Include(r => r.MenuItems)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<Restaurant?> GetRestaurantWithSpecificMenuItemByIdAsync
            (Guid RestaurantId,Guid menuItemId, CancellationToken cancellationToken = default)
        {
            return await context.Restaurants.Include(r=>r.MenuItems.Where(mi=>mi.Id==menuItemId))
                .FirstOrDefaultAsync(r => r.Id == RestaurantId, cancellationToken);
        }

        public async Task<Restaurant?> GetRestaurantWithTagsAsync
            (Guid RestaurantId,CancellationToken cancellationToken = default)
        {
            return await context.Restaurants.Include(x=>x.RestaurantTags).ThenInclude(rt=>rt.Tag)
                .FirstOrDefaultAsync(r => r.Id == RestaurantId, cancellationToken);
        }

        public void Update(Restaurant enttiy) => context.Update(enttiy);
        
        public void Create(Restaurant entity) => context.Add(entity);

        public void Delete(Restaurant entity) => context.Remove(entity);

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await context.Restaurants.CountAsync(cancellationToken);
        }


        private IQueryable<Restaurant> ApplySpecifications(Specification<Restaurant> spec)
        {
            var query = SpecificationEvaluator<Restaurant>.GetQuery(context.Restaurants, spec);

            return query;
        }

      
    }
}
