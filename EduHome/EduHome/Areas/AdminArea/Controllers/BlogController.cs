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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _context.Blogs.ToListAsync();
            return View(blogs);
        }

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (!ModelState.IsValid) return View();
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            if (!blog.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View();
            }

            if (!blog.Photo.CheckFileSize(10000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "_" + blog.Photo.FileName;

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/blog", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await blog.Photo.CopyToAsync(stream);
            }

            blog.Image = fileName;



            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Blog blog = await _context.Blogs.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (blog == null) return NotFound();

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/blog", blog.Image);

            Helper.DeleteFile(path);

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Detail
        public async Task<IActionResult> Detail(int id)
        {
            Blog blog = await _context.Blogs.Where(m => m.Id == id).FirstOrDefaultAsync();

            return View(blog);
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            Blog blog = await _context.Blogs
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
            return View(blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Blog blog)
        {
            if (!ModelState.IsValid) return View();
            var dbBlog = await _context.Blogs.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (dbBlog == null) return NotFound();

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();

            if (!blog.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbBlog);
            }

            if (!blog.Photo.CheckFileSize(800))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(dbBlog);
            }
            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/blog", dbBlog.Image);
            Helper.DeleteFile(path);

            string fileName = Guid.NewGuid().ToString() + "_" + blog.Photo.FileName;

            path = Helper.GetFilePath(_env.WebRootPath, "assets/img/blog", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await blog.Photo.CopyToAsync(stream);
            }

            dbBlog.Image = fileName;
            dbBlog.BlogName = blog.BlogName;
            dbBlog.Author = blog.Author;
            dbBlog.Desc = blog.Desc;
            dbBlog.Date = blog.Date;


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
