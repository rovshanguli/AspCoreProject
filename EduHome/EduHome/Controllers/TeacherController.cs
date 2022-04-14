using EduHome.Data;
using EduHome.Models;
using EduHome.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        public TeacherController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> Detail(int id)
        {

            Teacher teacher = await _context.Teachers
                .Include(m => m.TeacherSkills)
                .ThenInclude(m => m.Skill)
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
            List<TeacherSkill> teacherSkills = await _context.TeacherSkills.Include(m => m.Skill).Where(m => m.TeacherId == id).ToListAsync();
            List<Skill> skillsData = new List<Skill>();
            List<int> skillsPercent = new List<int>();
            foreach (var skill in teacherSkills)
            {
                Skill skills = await _context.Skills.Where(m => m.Id == skill.SkillId).FirstOrDefaultAsync();
                skillsData.Add(skills);
                skillsPercent.Add(skill.Percent);
            }
            TeacherDetailVM teacherDetail = new TeacherDetailVM
            {
                teacher = teacher,
                skills = skillsData,
                percents = skillsPercent
            };
            return View(teacherDetail);
        }

    }
}
