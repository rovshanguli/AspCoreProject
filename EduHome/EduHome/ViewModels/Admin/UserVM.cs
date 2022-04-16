using EduHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewModels.Admin
{
    public class UserVM
    {
        public List<AppUser> activeUsers { get; set; }
        public List<AppUser> deactiveUsers { get; set; }
    }
}
