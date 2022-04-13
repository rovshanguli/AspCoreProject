﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
