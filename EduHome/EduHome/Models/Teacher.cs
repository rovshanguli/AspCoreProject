using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Teacher : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Position { get; set; }
        public string About { get; set; }
        public string Degree { get; set; }
        public int Experience { get; set; }
        public string Faculty { get; set; }

        public string Mail { get; set; }
        public string Number { get; set; }
        public string Skype { get; set; }
        public List<TeacherSkill> TeacherSkills { get; set; }
    }
}
