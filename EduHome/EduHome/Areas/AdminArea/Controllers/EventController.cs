using EduHome.Data;
using EduHome.Models;
using EduHome.Utilities.Helpers;
using EduHome.ViewModels.Admin;
using LessonMigration.Utilities.File;
using LessonMigration.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public EventController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Event> events = await _context.Events.Include(m => m.EventDetail).ToListAsync();
            return View(events);
        }

        #region Create
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventVM eventVM)
        {

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
            if (!ModelState.IsValid) return View();

            if (!eventVM.detailPhoto.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View();
            }

            if (!eventVM.detailPhoto.CheckFileSize(10000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + "_" + eventVM.detailPhoto.FileName;
            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/event", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await eventVM.detailPhoto.CopyToAsync(stream);
            }
            EventDetail eventDetail = new EventDetail
            {
                
                DetailImage = fileName,
                Adress = eventVM.eventDetail.Adress,
                Desc = eventVM.eventDetail.Desc
            };
            await _context.EventDetails.AddAsync(eventDetail);
            await _context.SaveChangesAsync();


            if (!eventVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View();
            }

            if (!eventVM.Photo.CheckFileSize(10000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }

            fileName = Guid.NewGuid().ToString() + "_" + eventVM.detailPhoto.FileName;
            path = Helper.GetFilePath(_env.WebRootPath, "assets/img/event", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await eventVM.detailPhoto.CopyToAsync(stream);
            }

            List<EventDetail> eventDetails = await _context.EventDetails.ToListAsync();
            Event @event = new Event
            {
                Image = fileName,
                Name = eventVM.events.Name,
                City = eventVM.events.City,
                Start = eventVM.events.Start,
                End = eventVM.events.End,
                EventDetailId = eventDetails.LastOrDefault().Id
            };


            await _context.Events.AddAsync(@event);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Delete
        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Event @event = await _context.Events.Where(m => m.Id == id).Include(m => m.EventDetail).FirstOrDefaultAsync();

            if (@event == null) return NotFound();

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/course", @event.Image);

            Helper.DeleteFile(path);

            string pathDetail = Helper.GetFilePath(_env.WebRootPath, "assets/img/course", @event.EventDetail.DetailImage);
            Helper.DeleteFile(pathDetail);

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #endregion
        #region Detail
        public async Task<IActionResult> Detail(int id)
        {
            Event events = await _context.Events.Where(m => m.Id == id).Include(m => m.EventDetail).FirstOrDefaultAsync();
            return View(events);
        }
        #endregion
        #region Edit
        public async Task<IActionResult> Edit(int Id)
        {
            var @event = await GetEventById(Id);
            EventVM eventVM = new EventVM
            {
                  events = @event,
                  eventDetail = @event.EventDetail
            };
            if (eventVM == null) return NotFound();
            return View(eventVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, EventVM eventVM)
        {
            var dbEvents = await GetEventById(Id);
            if (dbEvents == null) return NotFound();
            if (!ModelState.IsValid) return View();
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();

            if (!eventVM.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbEvents);
            }

            if (!eventVM.Photo.CheckFileSize(800))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(dbEvents);
            }
            if (ModelState["DetailPhoto"].ValidationState == ModelValidationState.Invalid) return View();

            if (!eventVM.detailPhoto.CheckFileType("image/"))
            {
                ModelState.AddModelError("DetailPhoto", "Image type is wrong");
                return View(dbEvents);
            }

            if (!eventVM.detailPhoto.CheckFileSize(800))
            {
                ModelState.AddModelError("DetailPhoto", "Image size is wrong");
                return View(dbEvents);
            }

            string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/event", dbEvents.Image);
            Helper.DeleteFile(path);

            string pathDetail = Helper.GetFilePath(_env.WebRootPath, "assets/img/event", dbEvents.EventDetail.DetailImage);
            Helper.DeleteFile(pathDetail);

            string fileName = Guid.NewGuid().ToString() + "_" + eventVM.Photo.FileName;
            path = Helper.GetFilePath(_env.WebRootPath, "assets/img/event", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await eventVM.Photo.CopyToAsync(stream);
            }
            dbEvents.Image = fileName;
            dbEvents.Start = eventVM.events.Start;
            dbEvents.Name = eventVM.events.Name;
            dbEvents.End = eventVM.events.End;
            fileName = Guid.NewGuid().ToString() + "_" + eventVM.detailPhoto.FileName;
            path = Helper.GetFilePath(_env.WebRootPath, "assets/img/event", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await eventVM.detailPhoto.CopyToAsync(stream);
            }
            dbEvents.EventDetail.DetailImage = fileName;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helper
        private async Task<Event> GetEventById(int Id)
        {
            return await _context.Events.Where(m => m.Id == Id).Include(m => m.EventDetail).FirstOrDefaultAsync();
        }
        #endregion
    }
}
