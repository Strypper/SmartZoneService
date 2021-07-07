using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SmartZone.Entities
{
    public class User : IdentityUser
    {
        public string Guid { get; set; }
        [PersonalData]
        public string? FullName { get; set; } = string.Empty;
        public string? Policies { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
