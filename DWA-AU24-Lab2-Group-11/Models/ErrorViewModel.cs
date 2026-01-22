namespace DWA_AU24_Lab2_Group_11.Models
{
    /// <summary>
    /// View model for displaying error information to users.
    /// Used by the Error view to show request tracking information.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the unique request identifier for troubleshooting.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Gets whether the request ID should be displayed to the user.
        /// Returns true if the RequestId is not null or empty.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
