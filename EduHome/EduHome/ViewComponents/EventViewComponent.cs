using EduHome.Data;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewComponents
{
  

    public class EventViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public EventViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Event> events = await _context.Events
                .OrderByDescending(m => m.Id)
                .Include(m => m.EventDetail)
                .Take(4)
                .ToListAsync();
            return (await Task.FromResult(View(events)));
        }
    }
}
