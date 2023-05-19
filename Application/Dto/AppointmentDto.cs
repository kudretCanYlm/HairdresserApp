namespace Appointment.Application.Dto
{
	public class AppointmentDto
	{

		public Guid Id { get; set; }
		public string AppointmentState { get; set; }
		public string Notes { get; set; }
		public DateTime AppointmentDate { get; set; }
		public TimeSpan AppointmentStartTime { get; set; }
		public TimeSpan AppointmentEndTime { get; set; }
		public Guid HairdresserServiceId { get; set; }
		public string HairdresserServiceName { get; set; }
		public Guid HairdresserId { get; set; }
		public string HairdresserName { get; set; }
	}
}
