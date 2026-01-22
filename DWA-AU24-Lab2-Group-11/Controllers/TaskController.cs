using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DWA_AU24_Lab2_Group_11.Data;
using DWA_AU24_Lab2_Group_11.Models;
using Task = DWA_AU24_Lab2_Group_11.Models.Task;
using Microsoft.AspNetCore.Authorization;

namespace DWA_AU24_Lab2_Group_11.Controllers
{
    /// <summary>
    /// Controller for managing farm task CRUD operations.
    /// </summary>
    [Authorize]
    public class TaskController : Controller
    {
        private readonly FarmTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the TaskController.
        /// </summary>
        /// <param name="context">Database context for accessing task data.</param>
        public TaskController(FarmTrackContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays a list of all tasks with their associated planting schedules.
        /// </summary>
        /// <returns>The index view with all tasks.</returns>
        public async Task<IActionResult> Index()
        {
            var farmTrackContext = _context.Task.Include(t => t.PlantingSchedule);
            return View(await farmTrackContext.ToListAsync());
        }

        /// <summary>
        /// Displays details for a specific task.
        /// </summary>
        /// <param name="id">The task ID.</param>
        /// <returns>The details view or NotFound if task doesn't exist.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .Include(t => t.PlantingSchedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        /// <summary>
        /// Displays the form to create a new task.
        /// </summary>
        /// <returns>The create form view.</returns>
        public IActionResult Create()
        {
            ViewData["PlantingScheduleId"] = new SelectList(_context.PlantingSchedule, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return View();
        }

        /// <summary>
        /// Creates a new task from form data.
        /// </summary>
        /// <param name="task">The task data from the form.</param>
        /// <returns>Redirects to index on success, or returns the form with validation errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaskName,TaskDescription,TaskDate,IsCompleted,UserId,PlantingScheduleId")] Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlantingScheduleId"] = new SelectList(_context.PlantingSchedule, "Id", "Id", task.PlantingScheduleId);
            return View(task);
        }

        /// <summary>
        /// Displays the form to edit an existing task.
        /// </summary>
        /// <param name="id">The task ID to edit.</param>
        /// <returns>The edit form view or NotFound if task doesn't exist.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["PlantingScheduleId"] = new SelectList(_context.PlantingSchedule, "Id", "Id", task.PlantingScheduleId);
            return View(task);
        }

        /// <summary>
        /// Updates an existing task from form data.
        /// </summary>
        /// <param name="id">The task ID being edited.</param>
        /// <param name="task">The updated task data from the form.</param>
        /// <returns>Redirects to index on success, or returns the form with validation errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TaskName,TaskDescription,TaskDate,IsCompleted,UserId,PlantingScheduleId")] Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
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
            ViewData["PlantingScheduleId"] = new SelectList(_context.PlantingSchedule, "Id", "Id", task.PlantingScheduleId);
            return View(task);
        }

        /// <summary>
        /// Displays the delete confirmation page for a task.
        /// </summary>
        /// <param name="id">The task ID to delete.</param>
        /// <returns>The delete confirmation view or NotFound if task doesn't exist.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .Include(t => t.PlantingSchedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        /// <summary>
        /// Deletes a task after confirmation.
        /// </summary>
        /// <param name="id">The task ID to delete.</param>
        /// <returns>Redirects to the index page.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Task.FindAsync(id);
            if (task != null)
            {
                _context.Task.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks if a task exists in the database.
        /// </summary>
        /// <param name="id">The task ID to check.</param>
        /// <returns>True if the task exists, false otherwise.</returns>
        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.Id == id);
        }
    }
}
