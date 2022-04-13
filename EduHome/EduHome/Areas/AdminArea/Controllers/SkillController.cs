using EduHome.Data;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SkillController : Controller
    {
        private readonly AppDbContext _context;
        public SkillController(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            List<Skill> skills = await _context.Skills.ToListAsync();
            return View(skills);
        }

        #region Create
        public
            IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isExist = _context.Skills.Any(m => m.Name.ToLower().Trim() == skill.Name.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu skill artiq movcuddur");
                return View();
            }

            await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            Skill skill = await _context.Skills.Where(m => m.Id == Id).FirstOrDefaultAsync();

            if (skill == null) return NotFound();

            //category.IsDeleted = true;
            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #endregion

    }
}
