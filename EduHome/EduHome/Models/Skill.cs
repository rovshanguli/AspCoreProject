using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome.Models
{
    public class Skill : BaseEntity
    {
        public string Name { get; set; }
        public List<TeacherSkill> Teachers { get; set; }
        [NotMapped]
        public bool IsSelected { get; set; }
    }
}
