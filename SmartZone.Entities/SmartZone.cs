using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartZone.Entities
{
    public class SmartZone : BaseEntity 
    {
        public string Guid { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Photos { get; set; } = string.Empty;
        public string? CoverPhoto { get; set; } = string.Empty;
        public string? FacebookLink { get; set; } = string.Empty;
        public string? Logo { get; set; } = string.Empty;
        public int? OwnerId { get; set; } = null;
        public string? IdPrefix { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public bool IsExpired { get; set; }

        // A chain-store may have many stores/branches
        public virtual ICollection<Store> Stores { get; set; } = new HashSet<Store>();
    }
}
