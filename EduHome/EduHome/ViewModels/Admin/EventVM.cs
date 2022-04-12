using EduHome.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewModels.Admin
{
    public class EventVM
    {
        public Event events { get; set; }
        public EventDetail eventDetail { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public IFormFile detailPhoto { get; set; }
    }
}
