using SmartZone.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartZone.DataObjects
{
    public class ItemDTO : BaseDTOEntity<int>
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Size? Size { get; set; } = Entities.Size.MEDIUM;
        public string Image { get; set; } = String.Empty;
        public string? Description { get; set; } = String.Empty;
        public int? Calories { get; set; } = 0;
        public DateTime? PrepareTime { get; set; } = null;

        public virtual ICollection<OrderDTO> OrderDTOs { get; set; } = Array.Empty<OrderDTO>();

        //public ICollection<Material> Materials { get; set; }
        //option
    }
}
