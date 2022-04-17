using EduHome.Data;
using EduHome.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin,Moderator")]
    public class NoticeController : Controller
    {

        private readonly AppDbContext _context;
        
        public NoticeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Notice> notices = await _context.Notices.ToListAsync();

            return View(notices);
        }

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Notice notice)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isExist = _context.Notices.Any(m => m.Desc.ToLower().Trim() == notice.Desc.ToLower().Trim());

            await _context.Notices.AddAsync(notice);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            Notice notice = await _context.Notices.Where(m => m.Id == Id).FirstOrDefaultAsync();

            if (notice == null) return NotFound();
            _context.Notices.Remove(notice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Edit
        #region Edit
        public async Task<IActionResult> Edit(int Id)
        {
            Notice notice = await _context.Notices.Where(m => m.Id == Id).FirstOrDefaultAsync();
            if (notice == null) return NotFound();


            return View(notice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, Notice notice)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (Id != notice.Id) return NotFound();

            _context.Update(notice);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #endregion
        #region Detail

        #endregion

    }
}
