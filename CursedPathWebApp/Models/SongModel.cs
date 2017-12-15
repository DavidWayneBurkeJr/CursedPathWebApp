using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CursedPathWebApp.Models
{
    public class SongModel
    {
        public string Id { get; set; }
        public int OrderId { get; set; }
        [Required]
        public string SongTitle { get; set; }
        [DisplayFormat(DataFormatString = "{0:mm:ss}")]
        [Required]
        public DateTime Duration { get; set; }
        public string Comments { get; set; }
    }
}
