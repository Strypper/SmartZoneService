﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartZone.DataObjects
{
    public class RoleDTO : BaseDTOIdentity
    {
        public string Name { get; set; } = string.Empty;
    }
}
