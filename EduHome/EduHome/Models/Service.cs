﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Required]
        public string Header { get; set; }
        [Required]
        public string Desc { get; set; }
    }
}
