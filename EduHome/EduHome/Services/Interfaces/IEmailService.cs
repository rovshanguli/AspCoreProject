using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string emailTo, string userName, string html, string content);
    }
}
