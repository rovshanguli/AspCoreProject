using EduHome.Data;
using EduHome.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class SubscribeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public SubscribeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }
        public async Task<IActionResult> Subscribe(Subscribe subscripe)
        {
            Subscribe subscribe = new Subscribe
            {
                Email = subscripe.Email
            };




            if (subscripe == null) return RedirectToAction("Index", "Error");

            await _context.Subscribes.AddAsync(subscribe);
            await _context.SaveChangesAsync();
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("EduHome", "test.code.asgerov@gmail.com"));

            message.To.Add(new MailboxAddress("", subscripe.Email));

            message.Subject = "Thank you for Subscribe";

            string emailbody = string.Empty;

            using (StreamReader streamReader = new StreamReader(Path.Combine(_env.WebRootPath, "Templates", "Subscribe.html")))
            {
                emailbody = streamReader.ReadToEnd();
            }





            emailbody = emailbody.Replace("{{email}}", $"{subscripe.Email}");

            message.Body = new TextPart(TextFormat.Html) { Text = emailbody };

            using var smtp = new SmtpClient();

            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("test.code.asgerov@gmail.com", "testcode007");
            smtp.Send(message);


            smtp.Disconnect(true);


            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
