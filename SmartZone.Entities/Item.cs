using System;
using System.Collections.Generic;

namespace SmartZone.Entities
{
    public class Item : BaseEntity
    {
        public Store Store { get; set; }
        public int? StoreId { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public Size Size { get; set; } = Size.MEDIUM;
        public string? Description { get; set; }    
        public int? Calories { get; set; } 
        public DateTime? PrepareTime { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        //public ICollection<Material> Materials { get; set; }
        //option

    }

    public enum Size
    {
        SMALL, MEDIUM, LARGE, EXTRA
    }
}
