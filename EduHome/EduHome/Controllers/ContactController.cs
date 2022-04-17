using EduHome.Data;
using EduHome.Models;
using EduHome.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{

    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _env;

        public ContactController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _context = context;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Comment> comment = await _context.Comments.ToListAsync();
            CommentVM comments = new CommentVM
            {

                Comments = comment

            };
            return View(comments);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommentVM commentVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }



            Comment comments = new Comment
            {
                Name = commentVM.Name,
                Email = commentVM.Email,
                Subject = commentVM.Subject,
                TextMessage = commentVM.TextMessage
            };

            await _context.Comments.AddAsync(comments);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            Comment comment = await _context.Comments.Where(m => m.Id == Id).FirstOrDefaultAsync();

            if (comment == null) return NotFound();


            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
