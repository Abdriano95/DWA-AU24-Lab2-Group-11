using DWA_AU24_Lab2_Group_11.Data;
using DWA_AU24_Lab2_Group_11.Models;
using DWA_AU24_Lab2_Group_11.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace DWA_AU24_Lab2_Group_11.Controllers
{
    /// <summary>
    /// Controller for the main dashboard and home pages.
    /// Displays weather data, notifications, and pending tasks.
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FarmTrackContext _context;
        private readonly WeatherApiService _weatherApiService;
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Initializes a new instance of the HomeController.
        /// </summary>
        /// <param name="logger">Logger for diagnostic output.</param>
        /// <param name="context">Database context for accessing farm data.</param>
        /// <param name="weatherApiService">Service for fetching weather data.</param>
        /// <param name="userManager">ASP.NET Identity user manager.</param>
        public HomeController(ILogger<HomeController> logger, FarmTrackContext context, WeatherApiService weatherApiService, UserManager<User> userManager)
        {
            _logger = logger;
            _context = context;
            _weatherApiService = weatherApiService;
            _userManager = userManager;
        }

        /// <summary>
        /// Displays the main dashboard with weather, notifications, and pending tasks.
        /// </summary>
        /// <returns>The dashboard view.</returns>
        public async Task<IActionResult> Index()
        {

            // Fetch all unread notifications (ensure it's not null)
            var notifications = _context.Notification
                                         .Where(n => !n.IsRead)
                                         .ToList();

            if (notifications == null || !notifications.Any())
            {
                _logger.LogWarning("No unread notifications found.");
            }

            // Fetch Tasks where the date has passed or is now and IsCompleted is false
            var tasks = _context.Task
                                .Where(t => t.TaskDate <= DateTime.Now && !t.IsCompleted)
                                .ToList();

            if (tasks == null || !tasks.Any())
            {
                _logger.LogWarning("No tasks found for reminder.");
            }

            var user = await _userManager.GetUserAsync(User);
            WeatherData weatherData = null;

            if (user != null)
            {
                try
                {
                    // Fetch weather data using user's coordinates and location name
                    weatherData = await _weatherApiService.FetchWeatherAsync(user.Latitude, user.Longitude, user.Location);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to fetch weather data.");
                }
            }

            ViewBag.Notifications = notifications;
            ViewBag.Tasks = tasks;
            ViewBag.WeatherData = weatherData;
            ViewBag.FirstName = user?.FirstName; 

            return View(); 
        }

        /// <summary>
        /// Marks a notification as read.
        /// </summary>
        /// <param name="id">The notification ID to mark as read.</param>
        /// <returns>Redirects to the dashboard.</returns>
        [HttpPost]
        public IActionResult MarkAsRead(int id)
        {
            var notification = _context.Notification.Find(id);
            if (notification != null)
            {
                notification.IsRead = true; 
                _context.SaveChanges(); 
            }

            return RedirectToAction("Index"); 
        }

        /// <summary>
        /// Marks a task as completed.
        /// </summary>
        /// <param name="id">The task ID to mark as completed.</param>
        /// <returns>Redirects to the dashboard.</returns>
        [HttpPost]
        public IActionResult MarkTaskAsCompleted(int id)
        {
            var task = _context.Task.Find(id);
            if (task != null)
            {
                task.IsCompleted = true;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays the privacy policy page.
        /// </summary>
        /// <returns>The privacy view.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Displays the error page with request tracking information.
        /// </summary>
        /// <returns>The error view with request ID.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}