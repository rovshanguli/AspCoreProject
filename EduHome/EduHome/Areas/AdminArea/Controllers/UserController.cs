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
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(AppDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            List<AppUser> activeUsers = await _context.Users.Where(m => m.IsActivated == true).ToListAsync();
            List<AppUser> deactiveUsers = await _context.Users.Where(m => m.IsActivated == false).ToListAsync();

            var activeUsersRole = await _userManager.Users.Where(m => m.IsActivated == true ).ToListAsync();
            List<string> activeUsersRoles = new List<string>() { };
            foreach (var user in activeUsersRole)
            {
                var roles = await _userManager.GetRolesAsync(user);

            }

            UserVM userVM = new UserVM
            {
                activeUsers = activeUsers,
                deactiveUsers = deactiveUsers
            };
            return View(userVM);
        }

        public async Task<IActionResult> ChangeStatus(string id)
        {
            //AppUser user = await _context.Users.Where(m => m.Id == id).FirstOrDefaultAsync();
            var user = await _userManager.FindByIdAsync(id);

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


        public async Task<IActionResult> ChangeRole(string id)
        {

            var user = await _userManager.FindByIdAsync(id);


            var userAndRole = await _context.UserRoles.Where(m => m.UserId == id).FirstOrDefaultAsync();


            if(userAndRole.RoleId.ToString() == "581287e5-16c2-42bf-9ef2-64daac993e37")
            {
                userAndRole.RoleId = "1ebf7c11-d5d1-456c-a182-fc5f7ef9cdce";
            }
            else if(userAndRole.RoleId.ToString() == "1ebf7c11-d5d1-456c-a182-fc5f7ef9cdce")
            {
                userAndRole.RoleId = "581287e5-16c2-42bf-9ef2-64daac993e37";
            }
            else
            {
                NotFound();
            }


            //if (user == null)
            //{
            //    ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
            //    return RedirectToAction("ListRoles", "Admin");
            //}
            //else
            //{
            //    await _userManager.RemoveFromRoleAsync(user, currentRole.Result.Name);
            //    await _userManager.AddToRoleAsync(user, currentRole.Result.Name);
            //    return RedirectToAction("ListRoles", "Admin");
            //}
            return RedirectToAction(nameof(Index));
        }


    }
}
