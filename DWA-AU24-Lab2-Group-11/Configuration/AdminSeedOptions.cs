namespace DWA_AU24_Lab2_Group_11.Configuration
{
    /// <summary>
    /// Configuration options for seeding the initial admin user.
    /// Credentials should be stored in User Secrets (development) or environment variables (production).
    /// NEVER commit actual credentials to source control.
    /// </summary>
    public class AdminSeedOptions
    {
        /// <summary>
        /// Configuration section name in appsettings.json.
        /// </summary>
        public const string SectionName = "AdminSeed";

        /// <summary>
        /// Email address for the admin user account.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Password for the admin user account.
        /// Must meet ASP.NET Identity password requirements.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// First name for the admin user profile.
        /// </summary>
        public string FirstName { get; set; } = "Admin";

        /// <summary>
        /// Last name for the admin user profile.
        /// </summary>
        public string LastName { get; set; } = "User";

        /// <summary>
        /// Location name for the admin user profile (e.g., "Stockholm", "Gothenburg").
        /// Displayed in the weather widget.
        /// </summary>
        public string Location { get; set; } = "Stockholm";

        /// <summary>
        /// Latitude coordinate for the admin user's location.
        /// Used for fetching weather data. Default is Stockholm, Sweden.
        /// </summary>
        public double Latitude { get; set; } = 59.3293;

        /// <summary>
        /// Longitude coordinate for the admin user's location.
        /// Used for fetching weather data. Default is Stockholm, Sweden.
        /// </summary>
        public double Longitude { get; set; } = 18.0686;

        /// <summary>
        /// Validates that required credentials are configured.
        /// </summary>
        /// <returns>True if both Email and Password are non-empty.</returns>
        public bool IsConfigured => !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
    }
}
