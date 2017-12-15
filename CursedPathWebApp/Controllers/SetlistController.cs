using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CursedPathWebApp.Data;
using CursedPathWebApp.Models;
using Microsoft.AspNetCore.Http;

namespace CursedPathWebApp.Controllers
{
    public class SetlistController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public SetlistController(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<SongModel> model = new List<SongModel>();
            model = dbContext.Song.Select(u => new SongModel
            {
                Id = u.Id,
                OrderId = u.OrderId,
                SongTitle = u.SongTitle,
                Duration = u.Duration,
                Comments = u.Comments
            }).ToList();
            return View(model);
        }
        public void UpdateOrder(SongModel model, int fromPosition, string id, int toPosition)
        {

            using (dbContext)
            {
                var songList = dbContext.Song.ToList();
                dbContext.Song.Find(id).OrderId = toPosition;
                dbContext.SaveChanges();
            }
        }

        [HttpGet]
        public IActionResult AddEditSong (string id)
        {
            SongModel song = new SongModel();
            if (!String.IsNullOrEmpty(id))
            {

                var selectedSong = dbContext.Song.Find(id);
                if (selectedSong != null)
                {
                    song.Id = selectedSong.Id;
                    song.OrderId = selectedSong.OrderId;
                    song.SongTitle = selectedSong.SongTitle;
                    song.Comments = selectedSong.Comments;
                    song.Duration = selectedSong.Duration;
                }
                
            }
            song.OrderId = dbContext.Song.Count() + 1;
            return PartialView("_AddEditSong", song);
        }
        [HttpPost]
        public async Task<IActionResult> AddEditSong(string id, SongModel model)
        {
            if (ModelState.IsValid)
            {
                model.OrderId = dbContext.Song.Count() + 1;
                await dbContext.AddAsync(model);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public IActionResult DeleteSong(string id)
        {
           
            return PartialView("_DeleteSong");
        }
        [HttpPost]
        public IActionResult DeleteSong(string id, IFormCollection form)
        {
            SongModel song = new SongModel();
            using (dbContext)
            {
                song = dbContext.Song.Find(id);
                dbContext.Song.Remove(song);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}