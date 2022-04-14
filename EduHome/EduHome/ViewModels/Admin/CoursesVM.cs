using EduHome.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewModels
{
    public class CoursesVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [Required, MinLength(10)]
        public string Name { get; set; }
        [Required,MinLength(15,ErrorMessage ="15-den asagi ola bilmez")]
        public string Desc { get; set; }
        public string About { get; set; }
        public string Apply { get; set; }
        public string Certification { get; set; }
        public int FeatureId { get; set; }
        public CourseFeatures Feature { get; set; }
        public DateTime Start { get; set; }
        public int Duration { get; set; }
        public int ClassDuration { get; set; }
        public string Level { get; set; }
        public string Lanuguage { get; set; }
        public int Student { get; set; }
        public string Assesments { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
