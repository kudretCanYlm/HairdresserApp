namespace Notification.Api.Dto
{
	public class AppointmentNotificationDto
	{
		public Guid SendTo { get; set; }
		public Guid AppointmentId { get; set; }
		public string AppointmentState { get; set; }
		public DateTime AppointmentDate { get; set; }
		public TimeSpan AppointmentStartTime { get; set; }
		public TimeSpan AppointmentEndTime { get; set; }
		public string Notes { get; set; }

	}
}
