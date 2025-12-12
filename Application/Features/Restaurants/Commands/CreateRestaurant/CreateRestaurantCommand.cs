using Domain.Abstractions.Result;
using MediatR;

namespace Application.Features.Restaurants.Commands.CreateRestaurant
{
    public record CreateRestaurantCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string LogoUrl { get; set; }
    }

    
}
