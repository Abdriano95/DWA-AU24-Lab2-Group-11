using System.ComponentModel.DataAnnotations;

namespace DWA_AU24_Lab2_Group_11.Models
{
    /// <summary>
    /// Tracks the harvest status of a planting schedule.
    /// Records when a crop is harvested and calculates time until expected harvest.
    /// </summary>
    public class HarvestTracking
    {
        /// <summary>
        /// Gets or sets the unique identifier for the harvest tracking record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the actual date when the crop was harvested.
        /// Null if the crop has not been harvested yet.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? HarvestDate { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the associated planting schedule.
        /// </summary>
        public int PlantingScheduleId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the associated planting schedule.
        /// </summary>
        public PlantingSchedule? PlantingSchedule { get; set; }

        /// <summary>
        /// Gets the harvest status as a display string.
        /// Returns the harvest date if harvested, otherwise "Not harvested yet".
        /// </summary>
        public string HarvestStatus
        {
            get
            {
                return HarvestDate.HasValue
                    ? HarvestDate.Value.ToShortDateString()
                    : "Not harvested yet";
            }
        }

        /// <summary>
        /// Gets the time remaining until the expected harvest date.
        /// Returns a human-readable string like "5 days, 3 hours" or "Harvest time has passed!".
        /// </summary>
        public string? DaysUntilHarvest
        {
            get
            {
                if (PlantingSchedule?.ExpectedHarvestDate.HasValue == true)
                {
                    var timeSpan = PlantingSchedule.ExpectedHarvestDate.Value - DateTime.Now;

                    if (timeSpan.TotalSeconds <= 0)
                    {
                        return "Harvest time has passed!";
                    }

                    int remainingDays = timeSpan.Days;
                    int remainingHours = timeSpan.Hours;

                    if (remainingDays > 0)
                    {
                        return $"{remainingDays} days, {remainingHours} hours";
                    }
                    else
                    {
                        return $"{remainingHours} hours remaining";
                    }
                }
                return null;
            }
        }
    }
}
