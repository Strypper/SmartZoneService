
using System.Collections.Generic;

namespace SmartZone.Entities
{
    public class Customer : User
    {
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
