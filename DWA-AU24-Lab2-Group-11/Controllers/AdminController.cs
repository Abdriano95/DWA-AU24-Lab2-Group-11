using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DWA_AU24_Lab2_Group_11.Models;
using System.Threading.Tasks;
using System.Linq;

namespace DWA_AU24_Lab2_Group_11.Controllers
{
    /// <summary>
    /// Controller for administrative functions.
    /// Accessible only to users with the "Admin" role.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Initializes a new instance of the AdminController.
        /// </summary>
        /// <param name="userManager">ASP.NET Identity user manager.</param>
        /// <param name="roleManager">ASP.NET Identity role manager.</param>
        public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Displays a list of all users and their roles.
        /// </summary>
        /// <returns>The admin index view with user list.</returns>
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();

            var model = new Views.Admin.IndexModel
            {
                Users = users
            };

            return View(model);
        }

        /// <summary>
        /// Promotes a user to the Admin role.
        /// Creates the Admin role if it doesn't exist.
        /// </summary>
        /// <param name="id">The user ID to promote.</param>
        /// <returns>Redirects to the admin index page.</returns>
        [HttpPost]
        public async Task<IActionResult> MakeAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                if (!await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }

            return RedirectToAction("Index");
        }
    }
}

