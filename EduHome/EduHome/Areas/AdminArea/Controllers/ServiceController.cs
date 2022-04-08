using EduHome.Data;
using EduHome.Models;
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
    }
}
