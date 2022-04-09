using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class CourseFeatures
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public int Duration { get; set; }
        public int ClassDuration { get; set; }
        public string Level { get; set; }
        public string Lanuguage { get; set; }
        public int Student { get; set; }
        public string Assesments { get; set; }
    }
}
