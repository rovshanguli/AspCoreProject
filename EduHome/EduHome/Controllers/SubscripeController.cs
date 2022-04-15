using EduHome.Data;
using EduHome.Models;
using EduHome.ViewModels.Account;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace EduHome.Controllers
{
    public class SubscripeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        public SubscripeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IWebHostEnvironment env, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _context = context;
        }

        public async  Task<IActionResult> Subscribe(Subscripe subscripe)
        {
            Subscripe subscribe = new Subscripe
            {
                Email = subscripe.Email
            };




            if (subscripe == null) return RedirectToAction("Index", "Error");

            await _context.Subscripes.AddAsync(subscribe);
            await _context.SaveChangesAsync();
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("EduHome", "quliyevr879@gmail.com"));

            message.To.Add(new MailboxAddress("",subscripe.Email));

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
            smtp.Authenticate("quliyevr879@gmail.com", "1920Yevlax");
            smtp.Send(message);


            smtp.Disconnect(true);


            return RedirectToAction("Index", "Home");
        }
    }
}
