using EduHome.Data;
using EduHome.Models;
using EduHome.ViewModels.Admin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<AppUser> activeUsers = await _context.Users.Where(m => m.IsActivated == true).ToListAsync();
            List<AppUser> deactiveUsers = await _context.Users.Where(m => m.IsActivated == false).ToListAsync();


            UserVM userVM = new UserVM
            {
                activeUsers = activeUsers,
                deactiveUsers = deactiveUsers
            };
            return View(userVM);
        }

        public async Task<IActionResult> ChangeStatus(string id)
        {
            AppUser user = await _context.Users.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (!user.IsActivated)
            {
                user.IsActivated = true;
            }
            else
            {
                user.IsActivated = false;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
