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
    /// Controller for managing crop CRUD operations.
    /// </summary>
    [Authorize]
    public class CropsController : Controller
    {
        private readonly FarmTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the CropsController.
        /// </summary>
        /// <param name="context">Database context for accessing crop data.</param>
        public CropsController(FarmTrackContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays a list of all crops.
        /// </summary>
        /// <returns>The index view with all crops.</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Crop.ToListAsync());
        }

        /// <summary>
        /// Displays details for a specific crop.
        /// </summary>
        /// <param name="id">The crop ID.</param>
        /// <returns>The details view or NotFound if crop doesn't exist.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crop = await _context.Crop
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crop == null)
            {
                return NotFound();
            }

            return View(crop);
        }

        /// <summary>
        /// Displays the form to create a new crop.
        /// </summary>
        /// <returns>The create form view.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a new crop from form data.
        /// </summary>
        /// <param name="crop">The crop data from the form.</param>
        /// <returns>Redirects to index on success, or returns the form with validation errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,GrowingDurationInDays,OptimalClimate")] Crop crop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crop);
        }

        /// <summary>
        /// Displays the form to edit an existing crop.
        /// </summary>
        /// <param name="id">The crop ID to edit.</param>
        /// <returns>The edit form view or NotFound if crop doesn't exist.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crop = await _context.Crop.FindAsync(id);
            if (crop == null)
            {
                return NotFound();
            }
            return View(crop);
        }

        /// <summary>
        /// Updates an existing crop from form data.
        /// </summary>
        /// <param name="id">The crop ID being edited.</param>
        /// <param name="crop">The updated crop data from the form.</param>
        /// <returns>Redirects to index on success, or returns the form with validation errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,GrowingDurationInDays,OptimalClimate")] Crop crop)
        {
            if (id != crop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CropExists(crop.Id))
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
            return View(crop);
        }

        /// <summary>
        /// Displays the delete confirmation page for a crop.
        /// </summary>
        /// <param name="id">The crop ID to delete.</param>
        /// <returns>The delete confirmation view or NotFound if crop doesn't exist.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crop = await _context.Crop
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crop == null)
            {
                return NotFound();
            }

            return View(crop);
        }

        /// <summary>
        /// Deletes a crop after confirmation.
        /// Prevents deletion if the crop has associated tasks.
        /// </summary>
        /// <param name="id">The crop ID to delete.</param>
        /// <returns>Redirects to index on success, or with error message if deletion is blocked.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crop = await _context.Crop.FindAsync(id);

            // Check if the crop is associated with any planting schedules that have tasks
            var isAssociatedWithTask = await _context.PlantingSchedule
                .Include(ps => ps.Tasks)
                .AnyAsync(ps => ps.CropId == id && ps.Tasks.Any());

            if (isAssociatedWithTask)
            {
                TempData["ErrorMessage"] = "You cannot remove this crop, there is a task associated with this crop.";
                return RedirectToAction(nameof(Index));
            }

            _context.Crop.Remove(crop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks if a crop exists in the database.
        /// </summary>
        /// <param name="id">The crop ID to check.</param>
        /// <returns>True if the crop exists, false otherwise.</returns>
        private bool CropExists(int id)
        {
            return _context.Crop.Any(e => e.Id == id);
        }
    }
}
