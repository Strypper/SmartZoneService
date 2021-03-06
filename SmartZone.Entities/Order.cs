using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartZone.Entities
{
    public class Order : BaseEntity
    {
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }
        public Store Store { get; set; }
        public int StoreId { get; set; }
        public string? Note { get; set; } = string.Empty;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Food>? Foods { get; set; } = new HashSet<Food>();

        // public virtual ICollection<Discount>? Discounts { get; set; } = new HashSet<Discount>();
    }

    public enum OrderStatus
    {
        Pending = 0, Processing, Shipping, Ready, Unpaid, Done, Canceled
    }
}
