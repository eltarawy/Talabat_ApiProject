﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalabatG02.Core.Entities
{
    public class Department: BaseEntity
    {
        public string Name { get; set; }
        public DateOnly DateOfCreatioon { get; set; }
    }
}
