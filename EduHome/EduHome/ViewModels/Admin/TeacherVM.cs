using EduHome.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewModels.Admin
{
    public class TeacherVM
    {
        public Teacher Teacher { get; set; }
        public List<Skill> Skills { get; set; }
        public List<int> Percents { get; set; }
        [Required]
        public IFormFile Photo { get; set; }

    }
}
