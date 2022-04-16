using EduHome.Data;
using EduHome.Models;
using EduHome.Utilities.Helpers;
using EduHome.ViewModels;
using LessonMigration.Utilities.File;
using LessonMigration.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles ="Admin, Moderator")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CourseController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var AdminId = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            List<CourseDetail> courseDetails = new List<CourseDetail> { };
            var admin = _context.Users.Where(m => m.Id == "ae5e7c95 - 14e6 - 4578 - 92f6 - 154190fe9216");
            if(AdminId == "ae5e7c95-14e6-4578-92f6-154190fe9216")
            {
                  courseDetails = await _context.CourseDetails.Include(m => m.Feature).ToListAsync();
            }
            else
            {
                courseDetails = await _context.CourseDetails
                    .Where(m => m.UserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier))
                    .Include(m => m.Feature)
                    .ToListAsync();
            }


            return View(courseDetails);
        }

        #region Create
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CoursesVM coursesVM)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            if (!coursesVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View();
            }

            if (!coursesVM.Photo.CheckFileSize(10000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }




            string fileName = Guid.NewGuid().ToString() + "_" + coursesVM.Photo.FileName;

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/course", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await coursesVM.Photo.CopyToAsync(stream);
            }


            CourseFeatures courseFeatures = new CourseFeatures
            {
                Start = coursesVM.Start,
                Duration = coursesVM.Duration,
                ClassDuration = coursesVM.ClassDuration,
                Level = coursesVM.Level,
                Lanuguage = coursesVM.Lanuguage,
                Student = coursesVM.Student,
                Assesments = coursesVM.Assesments
            };
            await _context.CourseFeatures.AddAsync(courseFeatures);
            await _context.SaveChangesAsync();
            List<CourseFeatures> courseFeature = await _context.CourseFeatures.ToListAsync();
            CourseDetail courseDetail = new CourseDetail
            {
                Image = fileName,
                Name = coursesVM.Name,
                Desc = coursesVM.Desc,
                About = coursesVM.About,
                Apply = coursesVM.Apply,
                Certification = coursesVM.Certification,
                FeatureId = courseFeature.LastOrDefault().Id,
                UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier)

            };

            
            await _context.CourseDetails.AddAsync(courseDetail);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Detail
        public async Task<IActionResult> Detail(int id)
        {
            CourseDetail courseDetail = await _context.CourseDetails.Where(m => m.Id == id).Include(m => m.Feature).FirstOrDefaultAsync();

            return View(courseDetail);
        }
        #endregion
        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            CourseDetail courseDetail  = await _context.CourseDetails.FindAsync(id);

            if (courseDetail == null) return NotFound();

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/course", courseDetail.Image);

            Helper.DeleteFile(path);

            _context.CourseDetails.Remove(courseDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            CourseDetail courseDetail = await _context.CourseDetails
                .Where(m => m.Id == id)
                .Include(m => m.Feature)
                .FirstOrDefaultAsync();
            CoursesVM coursesVM = new CoursesVM
            {
                Image = courseDetail.Image,
                Name = courseDetail.Name,
                Desc = courseDetail.Desc,
                About = courseDetail.About,
                Apply = courseDetail.Apply,
                Certification = courseDetail.Certification,
                Start = courseDetail.Feature.Start,
                Duration = courseDetail.Feature.Duration,
                ClassDuration = courseDetail.Feature.ClassDuration,
                Level = courseDetail.Feature.Level,
                Lanuguage = courseDetail.Feature.Level,
                Student = courseDetail.Feature.Student,
                Assesments = courseDetail.Feature.Assesments
            };
            return View(coursesVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, CoursesVM courseVM)
        {
            if (!ModelState.IsValid) return View();
            var dbCourse = await GetCourseById(Id);
            if (dbCourse == null) return NotFound();

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();

            if (!courseVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbCourse);
            }

            if (!courseVM.Photo.CheckFileSize(800))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(dbCourse);
            }

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/course", dbCourse.Image);

            Helper.DeleteFile(path);


            string fileName = Guid.NewGuid().ToString() + "_" + courseVM.Photo.FileName;

            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img/course", fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await courseVM.Photo.CopyToAsync(stream);
            }

            dbCourse.Image = fileName;
            dbCourse.Name = courseVM.Name;
            dbCourse.Desc = courseVM.Desc;
            dbCourse.About = courseVM.About;
            dbCourse.Apply = courseVM.Apply;
            dbCourse.Certification = courseVM.Certification;
            dbCourse.Feature.Start = courseVM.Start;
            dbCourse.Feature.Duration = courseVM.Duration;
            dbCourse.Feature.ClassDuration = courseVM.ClassDuration;
            dbCourse.Feature.Level = courseVM.Level;
            dbCourse.Feature.Lanuguage = courseVM.Lanuguage;
            dbCourse.Feature.Student = courseVM.Student;
            dbCourse.Feature.Assesments = courseVM.Assesments;



            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<CourseDetail> GetCourseById(int Id)
        {
            return await _context.CourseDetails.Where(m => m.Id == Id).Include(m => m.Feature).FirstOrDefaultAsync();
        }
        #endregion

    }
}
