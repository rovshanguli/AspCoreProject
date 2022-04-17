using EduHome.Data;
using EduHome.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        public ServiceController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Service> services = await _context.Services.ToListAsync();

            return View(services);
        }
        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isExist = _context.Services.Any(m => m.Desc.ToLower().Trim() == service.Desc.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu service artiq movcuddur");
                return View();
            }

            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Detail
        public async Task<IActionResult> Detail(int id)
        {
            Service service = await _context.Services.Where(m => m.Id == id).FirstOrDefaultAsync();
            return View(service);
        }
        #endregion
        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            Service service = await _context.Services.Where(m => m.Id == Id).FirstOrDefaultAsync();

            if (service == null) return NotFound();
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Edit

        public async Task<IActionResult> Edit(int Id)
        {
            Service service = await _context.Services.Where(m => m.Id == Id).FirstOrDefaultAsync();
            if (service == null) return NotFound();


            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, Service service)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (Id != service.Id) return NotFound();

            Service dbService = await _context.Services.AsNoTracking().Where(m => m.Id == Id).FirstOrDefaultAsync();

            if (dbService.Header.ToLower().Trim() == service.Header.ToLower().Trim() && dbService.Desc.ToLower().Trim() == service.Desc.ToLower().Trim())
            {
                return RedirectToAction(nameof(Index));
            }

            bool isExist = _context.Services.Any(m => m.Header.ToLower().Trim() == service.Header.ToLower().Trim() && m.Desc.ToLower().Trim() == service.Desc.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu Service artiq movcuddur");
                return View();
            }

            
            _context.Update(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
