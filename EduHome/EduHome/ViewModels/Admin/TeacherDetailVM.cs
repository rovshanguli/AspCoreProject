﻿using EduHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewModels.Admin
{
    public class TeacherDetailVM
    {
        public Teacher teacher { get; set; }
        public List<Skill> skills { get; set; }
        public List<int> percents { get; set; }
    }
}
