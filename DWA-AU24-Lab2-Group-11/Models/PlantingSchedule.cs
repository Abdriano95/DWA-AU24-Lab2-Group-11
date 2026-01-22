using System.ComponentModel.DataAnnotations;

namespace DWA_AU24_Lab2_Group_11.Models
{
    /// <summary>
    /// Represents a planting schedule for a specific crop.
    /// Tracks when a crop was planted and calculates expected harvest dates.
    /// </summary>
    public class PlantingSchedule
    {
        /// <summary>
        /// Gets or sets the unique identifier for the planting schedule.
        /// </summary>
        [Display(Name = "Planting Schedule Id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the associated crop.
        /// </summary>
        public int CropId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the associated crop.
        /// </summary>
        public Crop? Crop { get; set; }

        /// <summary>
        /// Gets or sets the date when the crop was planted.
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Planting Date")]
        public DateTime PlantingDate { get; set; }

        /// <summary>
        /// Gets or sets the optimal planting date for this crop type.
        /// Calculated based on the crop type when the schedule is created.
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Optimal Planting Date")]
        public DateTime? OptimalPlantingDate { get; set; }

        /// <summary>
        /// Gets or sets the location where the crop is planted.
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// Gets the expected harvest date based on the planting date and crop growing duration.
        /// Returns null if the crop information is not available.
        /// </summary>
        [Display(Name = "Expected Harvest Date")]
        public DateTime? ExpectedHarvestDate
        {
            get
            {
                if (Crop != null)
                {
                    return PlantingDate.AddDays(Crop.EffectiveGrowingDurationInDays);
                }
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the collection of tasks associated with this planting schedule.
        /// </summary>
        public ICollection<Task>? Tasks { get; set; }
    }
}
