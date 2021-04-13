using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartZone.Entities
{
    class Item : BaseEntity
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public Size Size { get; set; }
        public string? Description { get; set; }
        public int? Calories { get; set; }
        public DateTime? PrepareTime { get; set; }
        //public ICollection<Material> Materials { get; set; }
        //option

    }

    public enum Size
    {
        SMALL, MEDIUM, LARGE, EXTRA
    }
}
