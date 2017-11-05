using System.ComponentModel.DataAnnotations;

namespace CursedPathWebApp.Models
{
    public class NewMessageViewModel
    {
        [Required]
        public string Content { get; set; }
    }
}