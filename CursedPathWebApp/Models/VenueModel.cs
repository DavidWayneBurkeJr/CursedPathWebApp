using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CursedPathWebApp.Models
{
    public class VenueModel
    {
        [Key]
        public int VenueId { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Location { get; set; }

        public int Rating { get; set; }
        public String Comments { get; set; }
    }
}
