using Application.Restaurants;

namespace Application.Common
{
    public record PagedResponse<T>
    (
     int PageIndex,
     int PageSize,
     int Count,
     IReadOnlyList<T> Data
    );

    
}
