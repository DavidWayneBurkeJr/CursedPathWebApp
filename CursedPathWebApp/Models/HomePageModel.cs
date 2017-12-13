using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursedPathWebApp.Models
{
    public class HomePageModel
    {
        public IEnumerable<BlogPost> RecentBlogPosts { get; set; }
    }
}
