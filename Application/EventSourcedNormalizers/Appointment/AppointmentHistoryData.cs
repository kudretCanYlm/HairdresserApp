using Events.Appointment.Enum;

namespace Appointment.Application.EventSourcedNormalizers.Appointment
{
	public class AppointmentHistoryData
	{
		public string Action { get; set; }
		public string Id { get; set; }
		public string AppointmentState { get; set; }
		public string Notes { get; set; }
		public DateTime? AppointmentDate { get; set; }
		public TimeSpan? AppointmentStartTime { get; set; }
		public TimeSpan? AppointmentEndTime { get; set; }
		public string UserId { get; set; }
		public string HairdresserServiceId { get; set; }
		public string HairdresserId { get; set; }
		public string Timestamp { get; set; }
		public string Who { get; set; }
	}
}
