using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartZone.DataObjects
{
    public class SmartZoneDTO : BaseDTOEntity
    {
        public string? Guid { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Photos { get; set; } = string.Empty;
        public string? CoverPhoto { get; set; } = string.Empty;
        public string? FacebookLink { get; set; } = string.Empty;
        public string? Logo { get; set; } = string.Empty;
        public string? OwnerId { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public bool IsExpired { get; set; }

        public virtual ICollection<StoreDTO> StoreDTOs { get; set; } = Array.Empty<StoreDTO>();
    }
}
