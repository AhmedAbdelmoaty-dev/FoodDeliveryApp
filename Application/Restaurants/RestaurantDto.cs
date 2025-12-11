using Domain.Enums;

namespace Application.Restaurants
{
    public record RestaurantDto(Guid RestaurantId, string Name, string Address
    , string LogoUrl, List<RestaurantTags> Tages);
}
