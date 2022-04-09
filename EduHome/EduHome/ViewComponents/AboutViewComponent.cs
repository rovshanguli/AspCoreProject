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
    public class AboutViewComponent : ViewComponent
    { 
        private readonly AppDbContext _context;
        public AboutViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            About about = await _context.Abouts.FirstOrDefaultAsync();
            return (await Task.FromResult(View(about)));
        }

    }
}
