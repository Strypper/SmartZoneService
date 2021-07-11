using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartZone.DataObjects
{
    public class IdentityUserDTO : BaseDTOIdentity
    {
        public string Guid { get; set; }
        [Required]
        public string? FullName { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        public string? Policies { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<string> Roles { get; set; } = Array.Empty<string>();
    }
}
