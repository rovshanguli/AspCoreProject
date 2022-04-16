using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class CourseDetail
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [Required,MinLength(10)]
        public string Name { get; set; }
        public string Desc { get; set; }
        public string About { get; set; }
        public string Apply { get; set; }
        public string Certification { get; set; }
        public int FeatureId { get; set; }
        public CourseFeatures Feature { get; set; }

        public string UserId { get; set; }
    }
}
