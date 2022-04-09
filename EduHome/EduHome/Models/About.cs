using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class About
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string CompanyName { get; set; }
        public string Desc { get; set; }
        public string Desc2 { get; set; }
        public string Image { get; set; }
        [NotMapped]
        [Required]
        public IFormFile photo { get; set; }

    }
}
