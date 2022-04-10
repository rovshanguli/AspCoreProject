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
    public class NoticeViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public NoticeViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Notice> notices = await _context.Notices.ToListAsync();
            return (await Task.FromResult(View(notices)));
        }
    }
}
