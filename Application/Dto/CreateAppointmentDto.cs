namespace Appointment.Application.Dto
{
	public class CreateAppointmentDto
	{
		public string Notes { get; set; }
		public DateTime AppointmentDate { get; set; }
		public TimeSpan AppointmentStartTime { get; set; }
		public Guid UserId { get; set; }
		public Guid HairdresserServiceId { get; set; }
		public Guid HairdresserId { get; set; }
	}
}
