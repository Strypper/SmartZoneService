using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartZone.DataObjects
{
    public class EmployeeDTO : IdentityUserDTO
    {
        public int StoreId { get; set; }
    }
}
