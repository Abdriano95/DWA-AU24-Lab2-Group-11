using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DWA_AU24_Lab2_Group_11.Data;
using DWA_AU24_Lab2_Group_11.Models;
using Microsoft.AspNetCore.Authorization;

namespace DWA_AU24_Lab2_Group_11.Controllers
{
    /// <summary>
    /// Controller for managing harvest tracking CRUD operations.
    /// Tracks when crops are harvested and provides countdown to expected harvest dates.
    /// </summary>
    [Authorize]
    public class HarvestTrackingController : Controller
    {
        private readonly FarmTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the HarvestTrackingController.
        /// </summary>
        /// <param name="context">Database context for accessing harvest tracking data.</param>
        public HarvestTrackingController(FarmTrackContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays a list of all harvest tracking records with their associated planting schedules and crops.
        /// </summary>
        /// <returns>The index view with all harvest tracking records.</returns>
        public async Task<IActionResult> Index()
        {
            var harvestTrackings = await _context.HarvestTracking
                .Include(h => h.PlantingSchedule)
                    .ThenInclude(p => p.Crop) 
                .ToListAsync();

            return View(harvestTrackings);
        }

        /// <summary>
        /// Displays details for a specific harvest tracking record.
        /// </summary>
        /// <param name="id">The harvest tracking ID.</param>
        /// <returns>The details view or NotFound if record doesn't exist.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var harvestTracking = await _context.HarvestTracking
                .Include(h => h.PlantingSchedule)
                    .ThenInclude(p => p.Crop)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (harvestTracking == null)
            {
                return NotFound();
            }

            return View(harvestTracking);
        }

        /// <summary>
        /// Displays the form to create a new harvest tracking record.
        /// </summary>
        /// <returns>The create form view.</returns>
        public IActionResult Create()
        {
            ViewData["PlantingScheduleId"] = new SelectList(_context.PlantingSchedule, "Id", "Id");
            return View();
        }

        /// <summary>
        /// Creates a new harvest tracking record from form data.
        /// </summary>
        /// <param name="harvestTracking">The harvest tracking data from the form.</param>
        /// <returns>Redirects to index on success, or returns the form with validation errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlantingScheduleId")] HarvestTracking harvestTracking)
        {
            if (ModelState.IsValid)
            {
                var plantingSchedule = await _context.PlantingSchedule
                    .Include(p => p.Crop)
                    .FirstOrDefaultAsync(p => p.Id == harvestTracking.PlantingScheduleId);

                if (plantingSchedule == null)
                {
                    ModelState.AddModelError("", "Invalid Planting Schedule selected.");
                    ViewData["PlantingScheduleId"] = new SelectList(_context.PlantingSchedule, "Id", "Id");
                    return View(harvestTracking);
                }

                _context.Add(harvestTracking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PlantingScheduleId"] = new SelectList(_context.PlantingSchedule, "Id", "Id");
            return View(harvestTracking);
        }

        /// <summary>
        /// Marks a harvest tracking record as harvested with the current date.
        /// </summary>
        /// <param name="id">The harvest tracking ID to mark as harvested.</param>
        /// <returns>Redirects to the index page.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsHarvested(int id)
        {
            var harvestTracking = await _context.HarvestTracking.FindAsync(id);
            if (harvestTracking == null)
            {
                return NotFound();
            }

            harvestTracking.HarvestDate = DateTime.Now;

            _context.Update(harvestTracking);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Displays the form to edit an existing harvest tracking record.
        /// </summary>
        /// <param name="id">The harvest tracking ID to edit.</param>
        /// <returns>The edit form view or NotFound if record doesn't exist.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var harvestTracking = await _context.HarvestTracking.FindAsync(id);
            if (harvestTracking == null)
            {
                return NotFound();
            }

            ViewData["PlantingScheduleId"] = new SelectList(_context.PlantingSchedule, "Id", "Id", harvestTracking.PlantingScheduleId);
            return View(harvestTracking);
        }

        /// <summary>
        /// Updates an existing harvest tracking record from form data.
        /// </summary>
        /// <param name="id">The harvest tracking ID being edited.</param>
        /// <param name="harvestTracking">The updated harvest tracking data from the form.</param>
        /// <returns>Redirects to index on success, or returns the form with validation errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlantingScheduleId,HarvestDate")] HarvestTracking harvestTracking)
        {
            if (id != harvestTracking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(harvestTracking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HarvestTrackingExists(harvestTracking.Id))
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

            ViewData["PlantingScheduleId"] = new SelectList(_context.PlantingSchedule, "Id", "Id", harvestTracking.PlantingScheduleId);
            return View(harvestTracking);
        }

        /// <summary>
        /// Displays the delete confirmation page for a harvest tracking record.
        /// </summary>
        /// <param name="id">The harvest tracking ID to delete.</param>
        /// <returns>The delete confirmation view or NotFound if record doesn't exist.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var harvestTracking = await _context.HarvestTracking
                .Include(h => h.PlantingSchedule)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (harvestTracking == null)
            {
                return NotFound();
            }

            return View(harvestTracking);
        }

        /// <summary>
        /// Deletes a harvest tracking record after confirmation.
        /// </summary>
        /// <param name="id">The harvest tracking ID to delete.</param>
        /// <returns>Redirects to the index page.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var harvestTracking = await _context.HarvestTracking.FindAsync(id);
            if (harvestTracking != null)
            {
                _context.HarvestTracking.Remove(harvestTracking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks if a harvest tracking record exists in the database.
        /// </summary>
        /// <param name="id">The harvest tracking ID to check.</param>
        /// <returns>True if the record exists, false otherwise.</returns>
        private bool HarvestTrackingExists(int id)
        {
            return _context.HarvestTracking.Any(e => e.Id == id);
        }
    }
}
