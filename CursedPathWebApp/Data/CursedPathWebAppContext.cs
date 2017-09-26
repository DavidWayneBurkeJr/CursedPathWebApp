using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CursedPathWebApp.Models
{
    public class CursedPathWebAppContext : DbContext
    {
        public CursedPathWebAppContext (DbContextOptions<CursedPathWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<CursedPathWebApp.Models.Movie> Movie { get; set; }
    }
}
