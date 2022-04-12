using EduHome.Data;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        
        public EventController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Event> events = await _context.Events.Include(m => m.EventDetail).ToListAsync();
            return View(events);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Event @event = await _context.Events
                .Where(m => m.Id == id)
                .Include(m => m.EventDetail)
                .FirstOrDefaultAsync();
            return View(@event);
        }
    }
}
