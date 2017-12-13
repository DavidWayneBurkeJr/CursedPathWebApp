using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursedPathWebApp.Models
{
    public class SongModel
    {
        public string Id { get; set; }
        public int OrderId { get; set; }
        public string SongTitle { get; set; }
        public int Duration { get; set; }
        public string Comments { get; set; }
    }
}
