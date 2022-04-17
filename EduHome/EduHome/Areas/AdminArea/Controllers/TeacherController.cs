using EduHome.Data;
using EduHome.Models;
using EduHome.Utilities.Helpers;
using EduHome.ViewModels.Admin;
using LessonMigration.Utilities.File;
using LessonMigration.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public TeacherController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Teacher> teachers = await _context.Teachers.Where(m => m.IsActive == true).ToListAsync();
            return View(teachers);
        }



        #region Create
        public IActionResult Create(TeacherVM teacherVM)
        {
            List<Skill> skills = _context.Skills.ToList();
            teacherVM.Skills = skills;
            return View(teacherVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreateTeacher(TeacherVM teacherVM)
        {

            if (!teacherVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Image type is wrong");
                return NotFound();
            }

            if (!teacherVM.Photo.CheckFileSize(100000))
            {
                ModelState.AddModelError("Image", "Image size is wrong");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + "_" + teacherVM.Photo.FileName;

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/teacher", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await teacherVM.Photo.CopyToAsync(stream);
            }


            Teacher teacher = new Teacher
            {
                Image = fileName,
                Name = teacherVM.Teacher.Name,
                Position = teacherVM.Teacher.Position,
                About = teacherVM.Teacher.About,
                Degree = teacherVM.Teacher.Degree,
                Experience = teacherVM.Teacher.Experience,
                Faculty = teacherVM.Teacher.Faculty,
                Mail = teacherVM.Teacher.Mail,
                Number = teacherVM.Teacher.Number,
                Skype = teacherVM.Teacher.Skype,
            };
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();

            Teacher lastTeacher = await _context.Teachers.OrderByDescending(m => m.Id).FirstOrDefaultAsync();

            int count = 0;
            foreach (var skill in teacherVM.Skills.Where(m => m.IsSelected is true))
            {
                
                TeacherSkill teacherSkill = new TeacherSkill
                {
                    TeacherId = lastTeacher.Id,
                    SkillId = skill.Id,
                    Percent = teacherVM.Percents[count]
                };
                await _context.TeacherSkills.AddAsync(teacherSkill);
                count++;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Teacher teacher = await _context.Teachers.Where(m => m.Id == id).Include(m => m.TeacherSkills).FirstOrDefaultAsync();

            if (teacher == null) return NotFound();

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/teacher", teacher.Image);

            Helper.DeleteFile(path);



            teacher.IsActive = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Detail
        public async Task<IActionResult> Detail(int id)
        {
            Teacher teacher = await _context.Teachers.Where(m => m.Id == id).FirstOrDefaultAsync();
            List<TeacherSkill> teacherSkills = await _context.TeacherSkills.Where(m => m.TeacherId == id).ToListAsync();
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
        #endregion

    }
}
