using EduHome.Data;
using EduHome.Models;
using EduHome.Utilities.Pagination;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 9)
        {
            List<Blog> blogs = await _context.Blogs
                .Skip((page - 1) * take)
                .Take(take)
                .OrderBy(m => m.Id)
                .ToListAsync();


            var blogVM = GetMapDatas(blogs);

            int count = await GetPageCount(take);

            Paginate<BlogVM> result = new Paginate<BlogVM>(blogVM, page, count);

            return View(result);
        }





        private async Task<int> GetPageCount(int take)
        {
            var count = await _context.Blogs.CountAsync();

            return (int)Math.Ceiling((decimal)count / take);
        }

        private List<BlogVM> GetMapDatas(List<Blog> blogs)
        {
            List<BlogVM> bloglist = new List<BlogVM>();

            foreach (var blog in blogs)
            {
                BlogVM newBlog = new BlogVM
                {
                    Id = blog.Id,
                    Author = blog.Author,
                    Image = blog.Image,
                    Date = blog.Date,
                    Name = blog.BlogName,
                    Desc = blog.Desc


                };

                bloglist.Add(newBlog);
            }

            return bloglist;
        }

        #region Detail
        public async Task<IActionResult> Detail(int Id)
        {
            Blog blog = await _context.Blogs.Where(m => m.Id == Id).FirstOrDefaultAsync();
            return View(blog);
        }
        #endregion
    }
}
