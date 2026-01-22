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
using DWA_AU24_Lab2_Group_11.Services;
using DWA_AU24_Lab2_Group_11.Helpers;

namespace DWA_AU24_Lab2_Group_11.Controllers
{
    /// <summary>
    /// Controller for managing planting schedule CRUD operations.
    /// </summary>
    [Authorize]
    public class PlantingScheduleController : Controller
    {
        private readonly FarmTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the PlantingScheduleController.
        /// </summary>
        /// <param name="context">Database context for accessing planting schedule data.</param>
        public PlantingScheduleController(FarmTrackContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays a list of all planting schedules with their associated crops.
        /// </summary>
        /// <returns>The index view with all planting schedules.</returns>
        public async Task<IActionResult> Index()
        {
            var farmTrackContext = _context.PlantingSchedule.Include(p => p.Crop);
            return View(await farmTrackContext.ToListAsync());
        }

        /// <summary>
        /// Displays details for a specific planting schedule.
        /// </summary>
        /// <param name="id">The planting schedule ID.</param>
        /// <returns>The details view or NotFound if schedule doesn't exist.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantingSchedule = await _context.PlantingSchedule
                .Include(p => p.Crop)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plantingSchedule == null)
            {
                return NotFound();
            }

            return View(plantingSchedule);
        }

        /// <summary>
        /// Displays the form to create a new planting schedule.
        /// </summary>
        /// <returns>The create form view.</returns>
        public IActionResult Create()
        {
            ViewData["CropId"] = new SelectList(_context.Crop, "Id", "Name");
            return View();
        }

        /// <summary>
        /// Creates a new planting schedule from form data.
        /// Automatically calculates the optimal planting date based on crop type.
        /// </summary>
        /// <param name="plantingSchedule">The planting schedule data from the form.</param>
        /// <returns>Redirects to index on success, or returns the form with validation errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CropId,PlantingDate,Location")] PlantingSchedule plantingSchedule)
        {
            if (ModelState.IsValid)
            {
                var crop = await _context.Crop.FindAsync(plantingSchedule.CropId);
                if (crop == null)
                {
                    return NotFound("Crop not found.");
                }

                plantingSchedule.OptimalPlantingDate = CropTypeHelper.GetOptimalPlantingDate(crop.Type);

                _context.Add(plantingSchedule);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["CropId"] = new SelectList(_context.Crop, "Id", "Name", plantingSchedule.CropId);
            return View(plantingSchedule);
        }

        /// <summary>
        /// Displays the form to edit an existing planting schedule.
        /// </summary>
        /// <param name="id">The planting schedule ID to edit.</param>
        /// <returns>The edit form view or NotFound if schedule doesn't exist.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantingSchedule = await _context.PlantingSchedule.FindAsync(id);
            if (plantingSchedule == null)
            {
                return NotFound();
            }
            ViewData["CropId"] = new SelectList(_context.Crop, "Id", "Name", plantingSchedule.CropId);
            return View(plantingSchedule);
        }

        /// <summary>
        /// Updates an existing planting schedule from form data.
        /// Recalculates the optimal planting date if the crop is changed.
        /// </summary>
        /// <param name="id">The planting schedule ID being edited.</param>
        /// <param name="plantingSchedule">The updated planting schedule data from the form.</param>
        /// <returns>Redirects to index on success, or returns the form with validation errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CropId,PlantingDate,Location")] PlantingSchedule plantingSchedule)
        {
            if (id != plantingSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var crop = await _context.Crop.FindAsync(plantingSchedule.CropId);
                    if (crop == null)
                    {
                        return NotFound("Crop not found.");
                    }

                    plantingSchedule.OptimalPlantingDate = CropTypeHelper.GetOptimalPlantingDate(crop.Type);

                    _context.Update(plantingSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantingScheduleExists(plantingSchedule.Id))
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

            ViewData["CropId"] = new SelectList(_context.Crop, "Id", "Name", plantingSchedule.CropId);
            return View(plantingSchedule);
        }

        /// <summary>
        /// Displays the delete confirmation page for a planting schedule.
        /// </summary>
        /// <param name="id">The planting schedule ID to delete.</param>
        /// <returns>The delete confirmation view or NotFound if schedule doesn't exist.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantingSchedule = await _context.PlantingSchedule
                .Include(p => p.Crop)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plantingSchedule == null)
            {
                return NotFound();
            }

            return View(plantingSchedule);
        }

        /// <summary>
        /// Deletes a planting schedule after confirmation.
        /// Prevents deletion if the schedule has associated tasks.
        /// </summary>
        /// <param name="id">The planting schedule ID to delete.</param>
        /// <returns>Redirects to index on success, or with error message if deletion is blocked.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plantingSchedule = await _context.PlantingSchedule.FindAsync(id);

            var isAssociatedWithTask = await _context.Task
                .AnyAsync(t => t.PlantingScheduleId == id);

            if (isAssociatedWithTask)
            {
                TempData["ErrorMessage"] = "You cannot remove this planting schedule, there is a task associated with it.";
                return RedirectToAction(nameof(Index));
            }

            _context.PlantingSchedule.Remove(plantingSchedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks if a planting schedule exists in the database.
        /// </summary>
        /// <param name="id">The planting schedule ID to check.</param>
        /// <returns>True if the schedule exists, false otherwise.</returns>
        private bool PlantingScheduleExists(int id)
        {
            return _context.PlantingSchedule.Any(e => e.Id == id);
        }

        /// <summary>
        /// Gets the optimal planting date for a specific crop.
        /// Used by AJAX calls to update the form dynamically.
        /// </summary>
        /// <param name="cropId">The crop ID to get the optimal date for.</param>
        /// <returns>The optimal planting date as a string in yyyy-MM-dd format.</returns>
        [HttpGet]
        public async Task<IActionResult> GetOptimalPlantingDate(int cropId)
        {
            var crop = await _context.Crop.FindAsync(cropId);
            if (crop == null)
            {
                return NotFound("Crop not found.");
            }

            var optimalPlantingDate = CropTypeHelper.GetOptimalPlantingDate(crop.Type);

            return Content(optimalPlantingDate.ToString("yyyy-MM-dd"));
        }
    }
}
