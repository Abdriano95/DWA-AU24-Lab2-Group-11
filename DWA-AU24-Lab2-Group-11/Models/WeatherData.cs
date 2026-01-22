namespace DWA_AU24_Lab2_Group_11.Models
{
    /// <summary>
    /// Represents weather data for a specific location and time.
    /// Populated by the WeatherApiService from the Open-Meteo API.
    /// </summary>
    public class WeatherData
    {
        /// <summary>
        /// Gets or sets the unique identifier for the weather data record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the weather data was recorded.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the temperature in degrees Celsius.
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// Gets or sets the relative humidity percentage (0-100).
        /// </summary>
        public double Humidity { get; set; }

        /// <summary>
        /// Gets or sets the location name for display purposes.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the weather icon code for visual representation.
        /// Compatible with OpenWeatherMap icon URLs.
        /// </summary>
        public string Icon { get; set; }
    }
}
