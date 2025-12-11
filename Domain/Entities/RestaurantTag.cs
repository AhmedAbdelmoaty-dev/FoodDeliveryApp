namespace Domain.Entities
{
    public class RestaurantTag
    {
        public Guid RestaurantId { get; set; }
        
        public Restaurant Restaurant{get;set;}

        public Guid TagId { get; set; }
        
        public Tag Tag { get; set; }

    }
}
