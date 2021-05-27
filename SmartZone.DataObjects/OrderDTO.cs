using SmartZone.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartZone.DataObjects
{
    public class OrderDTO : BaseDTOEntity
    {
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public string? Note { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Pending;
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<FoodDTO>? FoodDTOs { get; set; } = Array.Empty<FoodDTO>();

        // public virtual ICollection<Discount>? Discounts { get; set; } = new HashSet<Discount>();
    }
}
