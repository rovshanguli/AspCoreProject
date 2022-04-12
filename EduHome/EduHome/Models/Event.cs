using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Event
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Image { get; set; }
        public string City { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int EventDetailId { get; set; }
        public EventDetail EventDetail { get; set; }

    }
}
