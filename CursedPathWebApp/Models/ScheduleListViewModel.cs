using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CursedPathWebApp.Models
{
    public class ScheduleListViewModel
    {
        [Key]
        public int ShowId { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        [Required]
        [Display(Name = "Venue")]
        public int VenueId { get; set; }
        public virtual VenueModel VenueModel { get; set; }
    }
}
