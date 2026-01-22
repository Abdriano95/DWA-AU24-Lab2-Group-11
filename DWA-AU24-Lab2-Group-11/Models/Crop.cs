using System.ComponentModel.DataAnnotations;

namespace DWA_AU24_Lab2_Group_11.Models
{
    /// <summary>
    /// Represents a crop that can be planted and tracked in the FarmTrack system.
    /// </summary>
    public class Crop
    {
        /// <summary>
        /// Gets or sets the unique identifier for the crop.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the crop (e.g., "Tomatoes", "Wheat").
        /// </summary>
        [Required(ErrorMessage = "The crop name is required")]
        [StringLength(100, ErrorMessage = "The crop name cannot exceed 100 characters.")]
        [Display(Name = "Crop Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type/category of the crop.
        /// </summary>
        [Required(ErrorMessage = "Please select a crop type.")]
        public CropType Type { get; set; }

        /// <summary>
        /// Gets or sets the optional custom growing duration in days.
        /// If not specified, the default duration for the crop type is used.
        /// </summary>
        [Range(1, 365, ErrorMessage = "Growing duration must be between 1 and 365 days.")]
        [Display(Name = "Growing Duration in days (Optional: Set a value or get a default value for the crop type)")]
        public int? GrowingDurationInDays { get; set; }

        /// <summary>
        /// Gets the effective growing duration in days.
        /// Returns the custom duration if set, otherwise the default duration for the crop type.
        /// </summary>
        public int EffectiveGrowingDurationInDays
        {
            get
            {
                return GrowingDurationInDays ?? (int)Type;
            }
        }

        /// <summary>
        /// Gets or sets the collection of planting schedules associated with this crop.
        /// </summary>
        public ICollection<PlantingSchedule>? PlantingSchedules { get; set; }
    }
}