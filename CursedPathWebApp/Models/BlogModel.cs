using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CursedPathWebApp.Data;

namespace CursedPathWebApp.Models
{
    public class BlogPost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Let the db generate the id
        public int Id { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }

        [Required]
        public virtual ApplicationUser Author { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ContentHTML { get; set; }

        [Required]
        public Boolean Deleted { get; set; }
    }
}
