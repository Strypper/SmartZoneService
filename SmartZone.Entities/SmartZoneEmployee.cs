using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartZone.Entities
{
    public class SmartZoneEmployee : SmartZoneUser
    {
        public string? StroreId { get; set; }
        public Store? Store { get; set; }
    }
}
