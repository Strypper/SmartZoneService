using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartZone.DataObjects
{
    public class SmartZoneDTO : BaseDTOEntity
    {
        public string? Name { get; set; } = String.Empty;
        public string? Photos { get; set; } = String.Empty;
        public string? CoverPhoto { get; set; } = String.Empty;
        public string? FacebookLink { get; set; } = String.Empty;
        public string? Logo { get; set; } = String.Empty;
        public string? OwnerId { get; set; } = String.Empty;
        public bool IsDeleted { get; set; }
        public bool IsExpired { get; set; }

        public virtual ICollection<AddressInfoDTO> AddressInfos { get; set; } = Array.Empty<AddressInfoDTO>();
    }
}
