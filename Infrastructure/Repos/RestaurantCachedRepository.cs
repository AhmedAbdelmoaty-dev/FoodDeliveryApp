using Application.Abstractions.Repositories;
using Application.Abstractions.Specification;
using Application.Common;
using Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrastructure.Repos
{
    public class RestaurantCachedRepository(IRestaurantRepository restaurantRepo, IDistributedCache cache) : IRestaurantRepository
    {

        public async Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var key = CacheKeys.RestaurantById(id);
           
            var cachedData = await cache.GetStringAsync(key,cancellationToken);

            if (cachedData is not null)
                return JsonSerializer.Deserialize<Restaurant>(cachedData);

          var restaurant= await restaurantRepo.GetByIdAsync(id, cancellationToken);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4),
                SlidingExpiration = TimeSpan.FromMinutes(30)
            };

           await cache.SetStringAsync(key,JsonSerializer.Serialize(restaurant), cacheOptions,cancellationToken);

            return restaurant;
        }

        public async Task<IReadOnlyList<Restaurant>> GetBySpecAsync(Specification<Restaurant> spec, CancellationToken cancellationToken = default)
        {
            var key = CacheKeys.RestaurantsBySpec(spec.ToStringFragments());

            var cachedData= await cache.GetStringAsync(key, cancellationToken);

            if (cachedData is not null)
                return JsonSerializer.Deserialize<IReadOnlyList<Restaurant>>(cachedData);

           var restaurants=  await  restaurantRepo.GetBySpecAsync(spec, cancellationToken);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(4),
                SlidingExpiration = TimeSpan.FromMinutes(30)
            };

           await cache.SetStringAsync(key,JsonSerializer.Serialize(restaurants), cacheOptions, cancellationToken);

            return restaurants;
        }
        public void Create(Restaurant entity)
        {
            restaurantRepo.Create(entity);
        }

        public void Delete(Restaurant entity)
        {
            restaurantRepo.Delete(entity);  
        }

        public void Update(Restaurant enttiy)
        {
            restaurantRepo.Update(enttiy);   
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await restaurantRepo.CountAsync(cancellationToken);
        }

        public async Task<Restaurant?> GetRestaurantWithListMenuItemsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
          return await restaurantRepo.GetRestaurantWithListMenuItemsByIdAsync(id,cancellationToken);
        }

        public async Task<Restaurant?> GetRestaurantWithSpecificMenuItemByIdAsync(Guid RestaurantId, Guid menuItemId, CancellationToken cancellationToken = default)
        {
          return  await restaurantRepo.GetRestaurantWithSpecificMenuItemByIdAsync(RestaurantId, menuItemId, cancellationToken);
        }

        public async Task<Restaurant?> GetRestaurantWithTagsAsync(Guid RestaurantId, CancellationToken cancellationToken = default)
        {
            return await restaurantRepo.GetRestaurantWithTagsAsync(RestaurantId, cancellationToken);
        }

        public Task<bool> IsExistAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void CreateMenuItem(MenuItem menuItem)
        {
            throw new NotImplementedException();
        }
    }
}
