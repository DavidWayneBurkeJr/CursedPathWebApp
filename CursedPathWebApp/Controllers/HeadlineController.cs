using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using CursedPathWebApp.Data;
using CursedPathWebApp.Models;

namespace CursedPathWebApp.Controllers
{
    [Authorize(Roles="Admin,Band Member")]
    public class HeadlineController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public HeadlineController(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx)
        {
            _userManager = userManager;
            _context = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string title, string contentHTML)
        {
            var post = new BlogPost
            {
                Author = await _userManager.GetUserAsync(HttpContext.User),
                Title = title,
                DatePosted = DateTime.Now,
                ContentHTML = contentHTML
            };

            await _context.BlogPosts.AddAsync(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("Manage");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            var post = await _context.FindAsync<BlogPost>(id);
            if (post.Author == user)
            {
                post.Deleted = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            ViewBag.userId = user.Id;
            return View(_context.BlogPosts.Where(post => !post.Deleted).OrderByDescending(post => post.DatePosted).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            var post = await _context.FindAsync<BlogPost>(id);
            if (post.Author == user)
            {
                return View(post);
            }
            return RedirectToAction("Manage");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, string title, string contentHTML)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            var post = await _context.FindAsync<BlogPost>(id);
            if (post.Author == user)
            {
                post.Title = title;
                post.ContentHTML = contentHTML;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Manage");
        }
    }
}
