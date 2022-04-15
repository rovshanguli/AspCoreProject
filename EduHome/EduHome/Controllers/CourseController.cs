using EduHome.Data;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            CourseDetail course = await _context.CourseDetails
                .Where(m => m.Id == id)
                .Include(m => m.Feature)
                .FirstOrDefaultAsync();
            return View(course);
        }


        public async Task<IActionResult> Search(string search)
        {
            List<CourseDetail> courses = await _context.CourseDetails.ToListAsync();

            List<CourseDetail> searchedCourses = new List<CourseDetail> { };

            foreach (var course in courses)
            {
                if (course.Name.ToLower().Contains(search.ToLower()))
                {
                    searchedCourses.Add(course);
                }
            }
            return View(searchedCourses);
        }
    }
}
