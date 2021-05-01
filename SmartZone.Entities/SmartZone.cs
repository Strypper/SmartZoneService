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
        public string? Name { get; set; } = String.Empty;
        public string? Photos { get; set; } = String.Empty;
        public string? CoverPhoto { get; set; } = String.Empty;
        public string? FacebookLink { get; set; } = String.Empty;
        public string? Logo { get; set; } = String.Empty;
        public string? OwnerId { get; set; } = String.Empty;
        public string? IdPrefix { get; set; } = String.Empty;
        public bool IsDeleted { get; set; }
        public bool IsExpired { get; set; }

        // A chain-store may have many stores/branches
        public virtual ICollection<Store> Stores { get; set; } = new HashSet<Store>();
    }
}
