using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CursedPathWebApp.Models;
using CursedPathWebApp.Data;

namespace CursedPathWebApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public IActionResult Index()
        {
            var model = new HomePageModel {
                RecentBlogPosts = _context.BlogPosts.Where(post => !post.Deleted).OrderByDescending(post => post.DatePosted).Take(3).ToList()
            };
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
