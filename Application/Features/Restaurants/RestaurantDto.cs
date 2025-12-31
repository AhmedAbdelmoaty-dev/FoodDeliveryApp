using Domain.Entities;

namespace Application.Features.Restaurants
{
    public record RestaurantDto(Guid Id, string Name, string Address
    , string LogoUrl,List<MenuItem>? MenuItems);
}
