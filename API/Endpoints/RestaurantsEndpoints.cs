using Application.Restaurants.Queries.GetRestaurant;
using MediatR;

namespace API.Endpoints
{
    public static class RestaurantsEndpoints
    {
        public static void MapRestaurantsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/restaurants");

            group.MapGet("/{id}", async (ISender _sender,Guid id) =>
            {
                var result = await _sender.Send(new GetRestaurantByIdQuery(id));

                
            });
        }
    }
}
