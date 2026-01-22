using System.ComponentModel.DataAnnotations;

namespace DWA_AU24_Lab2_Group_11.Models
{
    /// <summary>
    /// Represents a farm task that needs to be completed.
    /// Tasks can be standalone or associated with a planting schedule.
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Gets or sets the unique identifier for the task.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name/title of the task.
        /// </summary>
        [Display(Name = "Name")]
        public string TaskName { get; set; }

        /// <summary>
        /// Gets or sets the optional detailed description of the task.
        /// </summary>
        [Display(Name = "Description")]
        public string? TaskDescription { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the task is due.
        /// </summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "Task Date")]
        public DateTime TaskDate { get; set; }

        /// <summary>
        /// Gets or sets whether the task has been completed.
        /// </summary>
        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Gets or sets the optional foreign key to the associated planting schedule.
        /// </summary>
        public int? PlantingScheduleId { get; set; }

        /// <summary>
        /// Gets or sets the optional navigation property to the associated planting schedule.
        /// </summary>
        [Display(Name = "Planting Schedule")]
        public PlantingSchedule? PlantingSchedule { get; set; }
    }
}
