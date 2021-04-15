using System;

namespace SmartZone.Entities
{
    public class Item : BaseEntity
    {
        public Store Store { get; set; }
        public string? StoreId { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public Size Size { get; set; }
        public string? Description { get; set; }    
        public int? Calories { get; set; }      //em nghi nen de calo vao description
        public DateTime? PrepareTime { get; set; }

        //public ICollection<Material> Materials { get; set; }
        //option

    }

    public enum Size
    {
        SMALL, MEDIUM, LARGE, EXTRA
    }
}
