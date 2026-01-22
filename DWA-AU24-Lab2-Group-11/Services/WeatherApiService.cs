using DWA_AU24_Lab2_Group_11.Configuration;
using DWA_AU24_Lab2_Group_11.Models;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DWA_AU24_Lab2_Group_11.Services
{
    /// <summary>
    /// Service for fetching weather data from Open-Meteo API.
    /// Open-Meteo is free and requires no API key for non-commercial use.
    /// </summary>
    public class WeatherApiService
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherApiOptions _options;

        /// <summary>
        /// Initializes a new instance of the WeatherApiService.
        /// </summary>
        /// <param name="httpClient">The HTTP client for making API requests.</param>
        /// <param name="options">Weather API configuration options.</param>
        public WeatherApiService(HttpClient httpClient, IOptions<WeatherApiOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        /// <summary>
        /// Fetches current weather data for a specific geographic location.
        /// </summary>
        /// <param name="latitude">The latitude coordinate.</param>
        /// <param name="longitude">The longitude coordinate.</param>
        /// <param name="locationName">Optional friendly name for the location (e.g., "Stockholm").</param>
        /// <returns>Weather data for the specified location.</returns>
        public async Task<WeatherData> FetchWeatherAsync(double latitude, double longitude, string? locationName = null)
        {
            // Format coordinates with invariant culture to ensure decimal point (not comma)
            string lat = latitude.ToString(CultureInfo.InvariantCulture);
            string lon = longitude.ToString(CultureInfo.InvariantCulture);

            string url = $"{_options.BaseUrl}/forecast?latitude={lat}&longitude={lon}&current=temperature_2m,relative_humidity_2m,weather_code";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var weatherResponse = JsonSerializer.Deserialize<OpenMeteoResponse>(json);

            if (weatherResponse?.Current == null)
            {
                throw new InvalidOperationException("Invalid response from weather API.");
            }

            return new WeatherData
            {
                Date = DateTime.UtcNow,
                Temperature = weatherResponse.Current.Temperature,
                Humidity = weatherResponse.Current.RelativeHumidity,
                Location = !string.IsNullOrWhiteSpace(locationName) ? locationName : $"{latitude:F2}°, {longitude:F2}°",
                Icon = GetWeatherIcon(weatherResponse.Current.WeatherCode)
            };
        }

        /// <summary>
        /// Maps Open-Meteo weather codes to weather icon identifiers.
        /// Uses OpenWeatherMap icon codes for compatibility with existing UI.
        /// </summary>
        /// <param name="weatherCode">The WMO weather code from Open-Meteo.</param>
        /// <returns>An icon code compatible with OpenWeatherMap icon URLs.</returns>
        private static string GetWeatherIcon(int weatherCode)
        {
            // WMO Weather interpretation codes (WW)
            // https://open-meteo.com/en/docs
            return weatherCode switch
            {
                0 => "01d",           // Clear sky
                1 => "01d",           // Mainly clear
                2 => "02d",           // Partly cloudy
                3 => "03d",           // Overcast
                45 or 48 => "50d",    // Fog
                51 or 53 or 55 => "09d",  // Drizzle
                56 or 57 => "09d",    // Freezing drizzle
                61 or 63 or 65 => "10d",  // Rain
                66 or 67 => "13d",    // Freezing rain
                71 or 73 or 75 => "13d",  // Snow fall
                77 => "13d",          // Snow grains
                80 or 81 or 82 => "09d",  // Rain showers
                85 or 86 => "13d",    // Snow showers
                95 => "11d",          // Thunderstorm
                96 or 99 => "11d",    // Thunderstorm with hail
                _ => "03d"            // Default to cloudy
            };
        }
    }

    #region Open-Meteo API Response DTOs

    /// <summary>
    /// Root response from Open-Meteo API.
    /// </summary>
    public class OpenMeteoResponse
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; } = string.Empty;

        [JsonPropertyName("current")]
        public OpenMeteoCurrentWeather? Current { get; set; }
    }

    /// <summary>
    /// Current weather data from Open-Meteo API.
    /// </summary>
    public class OpenMeteoCurrentWeather
    {
        [JsonPropertyName("time")]
        public string Time { get; set; } = string.Empty;

        [JsonPropertyName("temperature_2m")]
        public double Temperature { get; set; }

        [JsonPropertyName("relative_humidity_2m")]
        public int RelativeHumidity { get; set; }

        [JsonPropertyName("weather_code")]
        public int WeatherCode { get; set; }
    }

    #endregion
}
