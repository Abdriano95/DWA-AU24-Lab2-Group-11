using System.ComponentModel.DataAnnotations;

namespace DWA_AU24_Lab2_Group_11.Models
{
    /// <summary>
    /// Represents a notification message for the user.
    /// Notifications are typically generated automatically for harvest reminders.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Gets or sets the unique identifier for the notification.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the notification message text.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the date when the notification was created.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime NotificationDate { get; set; }

        /// <summary>
        /// Gets or sets whether the notification has been read by the user.
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the associated planting schedule.
        /// </summary>
        public int PlantingScheduleId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the associated planting schedule.
        /// </summary>
        public PlantingSchedule PlantingSchedule { get; set; }
    }
}
