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
    /// Controller for managing growth history CRUD operations.
    /// Records historical data about completed crop growth cycles for analysis.
    /// </summary>
    [Authorize]
    public class GrowthHistoryController : Controller
    {
        private readonly FarmTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the GrowthHistoryController.
        /// </summary>
        /// <param name="context">Database context for accessing growth history data.</param>
        public GrowthHistoryController(FarmTrackContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays a list of all growth history records.
        /// </summary>
        /// <returns>The index view with all growth history records.</returns>
        public async Task<IActionResult> Index()
        {
            var growthHistories = await _context.GrowthHistory.ToListAsync();
            return View(growthHistories);
        }

        /// <summary>
        /// Displays the form to create a new growth history record.
        /// Only shows planting schedules that have been harvested.
        /// </summary>
        /// <returns>The create form view.</returns>
        public IActionResult Create()
        {
            var harvestedSchedules = _context.HarvestTracking
                .Where(ht => ht.HarvestDate.HasValue)
                .Select(ht => new
                {
                    ht.PlantingScheduleId,
                    DisplayName = $"{ht.PlantingSchedule.Crop.Name} (Planted: {ht.PlantingSchedule.PlantingDate.ToShortDateString()})"
                })
                .ToList();

            ViewBag.PlantingScheduleId = new SelectList(harvestedSchedules, "PlantingScheduleId", "DisplayName");

            return View();
        }

        /// <summary>
        /// Creates a new growth history record from form data.
        /// Automatically populates crop name, dates, and calculates days between planting and harvest.
        /// </summary>
        /// <param name="growthHistory">The growth history data from the form.</param>
        /// <returns>Redirects to index on success, or returns the form with validation errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlantingScheduleId,Notes")] GrowthHistory growthHistory)
        {
            if (ModelState.IsValid)
            {
                var plantingSchedule = await _context.PlantingSchedule
                    .Include(ps => ps.Crop)
                    .FirstOrDefaultAsync(ps => ps.Id == growthHistory.PlantingScheduleId);

                var harvestTracking = await _context.HarvestTracking
                    .FirstOrDefaultAsync(ht => ht.PlantingScheduleId == growthHistory.PlantingScheduleId);

                if (plantingSchedule == null || harvestTracking == null || !harvestTracking.HarvestDate.HasValue)
                {
                    ModelState.AddModelError("", "Invalid Planting Schedule or Harvest Date not set.");
                    return View(growthHistory);
                }

                growthHistory.CropName = plantingSchedule.Crop?.Name;
                growthHistory.PlantingDate = plantingSchedule.PlantingDate;
                growthHistory.HarvestDate = harvestTracking.HarvestDate.Value;
                growthHistory.DaysBetween = (growthHistory.HarvestDate - growthHistory.PlantingDate).Days;

                _context.Add(growthHistory);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(growthHistory);
        }

        /// <summary>
        /// Displays the form to edit an existing growth history record.
        /// Only the Notes field can be edited.
        /// </summary>
        /// <param name="id">The growth history ID to edit.</param>
        /// <returns>The edit form view or NotFound if record doesn't exist.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var growthHistory = await _context.GrowthHistory.FindAsync(id);
            if (growthHistory == null)
            {
                return NotFound();
            }

            return View(growthHistory);
        }

        /// <summary>
        /// Updates an existing growth history record from form data.
        /// Only allows updating the Notes field to preserve historical accuracy.
        /// </summary>
        /// <param name="id">The growth history ID being edited.</param>
        /// <param name="growthHistory">The updated growth history data from the form.</param>
        /// <returns>Redirects to index on success, or returns the form with validation errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Notes")] GrowthHistory growthHistory)
        {
            if (id != growthHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingGrowthHistory = await _context.GrowthHistory.FindAsync(id);
                    if (existingGrowthHistory == null)
                    {
                        return NotFound();
                    }

                    existingGrowthHistory.Notes = growthHistory.Notes;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrowthHistoryExists(growthHistory.Id))
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

            return View(growthHistory);
        }

        /// <summary>
        /// Displays the delete confirmation page for a growth history record.
        /// </summary>
        /// <param name="id">The growth history ID to delete.</param>
        /// <returns>The delete confirmation view or NotFound if record doesn't exist.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var growthHistory = await _context.GrowthHistory
                .Include(g => g.PlantingSchedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (growthHistory == null)
            {
                return NotFound();
            }

            return View(growthHistory);
        }

        /// <summary>
        /// Deletes a growth history record after confirmation.
        /// </summary>
        /// <param name="id">The growth history ID to delete.</param>
        /// <returns>Redirects to the index page.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var growthHistory = await _context.GrowthHistory.FindAsync(id);
            if (growthHistory != null)
            {
                _context.GrowthHistory.Remove(growthHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks if a growth history record exists in the database.
        /// </summary>
        /// <param name="id">The growth history ID to check.</param>
        /// <returns>True if the record exists, false otherwise.</returns>
        private bool GrowthHistoryExists(int id)
        {
            return _context.GrowthHistory.Any(e => e.Id == id);
        }
    }
}
