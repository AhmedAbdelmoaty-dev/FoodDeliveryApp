using Domain.Common;

namespace Domain.Entities
{
    public class Restaurant:BaseEntity
    {
       
        public string Name { get; set; }
        
        public string Address { get; set; }
       
        public string LogoUrl { get; set; }

        public List<RestaurantTag> RestaurantTags { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }    


    }

    
}
