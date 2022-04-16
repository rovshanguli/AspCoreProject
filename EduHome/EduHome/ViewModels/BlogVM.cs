using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewModels
{
    public class BlogVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        [Required, NotMapped]
        public IFormFile Photo { get; set; }
    }
}
