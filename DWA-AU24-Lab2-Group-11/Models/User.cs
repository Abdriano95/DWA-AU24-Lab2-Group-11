using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DWA_AU24_Lab2_Group_11.Models
{
    /// <summary>
    /// Represents a user in the FarmTrack system.
    /// Extends ASP.NET Core Identity with additional profile information.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        [StringLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        [StringLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets the user's full name (first and last name combined).
        /// </summary>
        public string Fullname => $"{FirstName} {LastName}";

        /// <summary>
        /// Gets or sets the user's location name (e.g., "Stockholm", "Gothenburg").
        /// Used for display purposes in the weather widget.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the latitude coordinate of the user's location.
        /// Used for fetching weather data.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude coordinate of the user's location.
        /// Used for fetching weather data.
        /// </summary>
        public double Longitude { get; set; }
    }
}
