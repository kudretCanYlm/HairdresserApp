using Events.Appointment.Enum;

namespace Appointment.Application.Dto
{
	public class GetAllAppointmentsForUserDto
	{
		public TimeSpan AppointmentStartTime { get; set; }
		public TimeSpan AppointmentEndTime { get; set; }
		public AppointmentStateEnum AppointmentState { get; set; }
	}
}
