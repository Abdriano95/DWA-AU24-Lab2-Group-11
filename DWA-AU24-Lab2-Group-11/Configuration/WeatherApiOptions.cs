namespace DWA_AU24_Lab2_Group_11.Configuration
{
    /// <summary>
    /// Configuration options for the Open-Meteo Weather API integration.
    /// </summary>
    public class WeatherApiOptions
    {
        /// <summary>
        /// Configuration section name in appsettings.json.
        /// </summary>
        public const string SectionName = "WeatherApi";

        /// <summary>
        /// Base URL for the Open-Meteo API.
        /// No API key required - Open-Meteo is free for non-commercial use.
        /// </summary>
        public string BaseUrl { get; set; } = "https://api.open-meteo.com/v1";
    }
}
