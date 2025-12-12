namespace Application.Features.Restaurants
{
    public record RestaurantDto(Guid RestaurantId, string Name, string Address
    , string LogoUrl);
}
