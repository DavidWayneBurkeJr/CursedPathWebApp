using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CursedPathWebApp.Data;

namespace CursedPathWebApp.Models
{
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScheduleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Schedule
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Schedule.Include(s => s.VenueModel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Schedule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleListViewModel = await _context.Schedule
                .Include(s => s.VenueModel)
                .SingleOrDefaultAsync(m => m.ShowId == id);
            if (scheduleListViewModel == null)
            {
                return NotFound();
            }

            return View(scheduleListViewModel);
        }

        // GET: Schedule/Create
        public IActionResult Create()
        {
            ViewData["VenueId"] = new SelectList(_context.Venue, "VenueId", "Location");
            return View();
        }

        // POST: Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShowId,Date,Time,VenueId")] ScheduleListViewModel scheduleListViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scheduleListViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VenueId"] = new SelectList(_context.Venue, "VenueId", "Location", scheduleListViewModel.VenueId);
            return View(scheduleListViewModel);
        }

        // GET: Schedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleListViewModel = await _context.Schedule.SingleOrDefaultAsync(m => m.ShowId == id);
            if (scheduleListViewModel == null)
            {
                return NotFound();
            }
            ViewData["VenueId"] = new SelectList(_context.Venue, "VenueId", "Location", scheduleListViewModel.VenueId);
            return View(scheduleListViewModel);
        }

        // POST: Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShowId,Date,Time,VenueId")] ScheduleListViewModel scheduleListViewModel)
        {
            if (id != scheduleListViewModel.ShowId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scheduleListViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleListViewModelExists(scheduleListViewModel.ShowId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["VenueId"] = new SelectList(_context.Venue, "VenueId", "Location", scheduleListViewModel.VenueId);
            return View(scheduleListViewModel);
        }

        // GET: Schedule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleListViewModel = await _context.Schedule
                .Include(s => s.VenueModel)
                .SingleOrDefaultAsync(m => m.ShowId == id);
            if (scheduleListViewModel == null)
            {
                return NotFound();
            }

            return View(scheduleListViewModel);
        }

        // POST: Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scheduleListViewModel = await _context.Schedule.SingleOrDefaultAsync(m => m.ShowId == id);
            _context.Schedule.Remove(scheduleListViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleListViewModelExists(int id)
        {
            return _context.Schedule.Any(e => e.ShowId == id);
        }
    }
}
