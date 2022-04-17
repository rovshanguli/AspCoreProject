using EduHome.Data;
using EduHome.Models;
using EduHome.Utilities.Helpers;
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
using System.Threading.Tasks;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AboutController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Edit()
        {
            About about = await _context.Abouts.FirstOrDefaultAsync();
            return View(about);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(About about)
        
        {
            var dbAbout = await _context.Abouts.FirstOrDefaultAsync();
            if (dbAbout == null) return NotFound();
            
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();

            if (!about.photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbAbout);
            }

            if (!about.photo.CheckFileSize(20000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(dbAbout);
            }

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/about", dbAbout.Image);

            Helper.DeleteFile(path);


            string fileName = Guid.NewGuid().ToString() + "_" + about.photo.FileName;

            string newPath = Helper.GetFilePath(_env.WebRootPath, "assets/img/about", fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await about.photo.CopyToAsync(stream);
            }

            dbAbout.Image = fileName;
            dbAbout.Header = about.Header;
            dbAbout.CompanyName = about.CompanyName;
            dbAbout.Desc = about.Desc;
            dbAbout.Desc2 = about.Desc2;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit));
        }

    }
}
