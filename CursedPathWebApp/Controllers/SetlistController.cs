using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CursedPathWebApp.Data;
using CursedPathWebApp.Models;

namespace CursedPathWebApp.Controllers
{
    public class SetlistController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public SetlistController(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }
        
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
        public void UpdateOrder(int id, int fromPosition, int toPosition, string direction)
        {
            
            if (direction == "back")
            {
                var movedSong = dbContext.Song
                            .Where(c => (toPosition <= c.OrderId && c.OrderId <= fromPosition))
                            .ToList();

                foreach (var song in movedSong)
                {
                    song.OrderId++;
                }
            }
            else
            {
                var movedSong = dbContext.Song
                            .Where(c => (fromPosition <= c.OrderId && c.OrderId <= toPosition))
                            .ToList();
                foreach (var song in movedSong)
                {
                    song.OrderId--;
                }
            }

            dbContext.Song.First(c => c.Id == id.ToString()).OrderId = toPosition;
        }
    }
}