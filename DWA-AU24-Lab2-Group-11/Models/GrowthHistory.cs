using System.ComponentModel.DataAnnotations;

namespace DWA_AU24_Lab2_Group_11.Models
{
    /// <summary>
    /// Represents a historical record of a completed crop growth cycle.
    /// Created when a crop is harvested to track growth performance over time.
    /// </summary>
    public class GrowthHistory
    {
        /// <summary>
        /// Gets or sets the unique identifier for the growth history record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the associated planting schedule.
        /// </summary>
        public int PlantingScheduleId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the associated planting schedule.
        /// </summary>
        public PlantingSchedule? PlantingSchedule { get; set; }

        /// <summary>
        /// Gets or sets the name of the crop that was grown.
        /// Stored separately for historical reference even if the crop is later deleted.
        /// </summary>
        public string CropName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date when the crop was planted.
        /// </summary>
        public DateTime PlantingDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the crop was harvested.
        /// </summary>
        public DateTime HarvestDate { get; set; }

        /// <summary>
        /// Gets or sets the number of days between planting and harvest.
        /// Automatically calculated when the record is created.
        /// </summary>
        public int DaysBetween { get; set; }

        /// <summary>
        /// Gets or sets optional notes about the growth cycle.
        /// </summary>
        public string? Notes { get; set; }
    }
}
