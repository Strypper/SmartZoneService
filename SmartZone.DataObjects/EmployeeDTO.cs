using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartZone.DataObjects
{
    class EmployeeDTO : BaseDTOEntity
    {
        public string Guid { get; set; }
        [Required]
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string? Role { get; set; }
        [Required]
        public string? Policies { get; set; }
    }
}
