using Events.Appointment.Enum;
using NetDevPack.Messaging;

namespace Appointment.Domain.Commands.Appointment
{
	public class AppointmentCommand:Command
	{
		public Guid Id { get; set; }
		public AppointmentStateEnum AppointmentState { get; set; }
		public string Notes { get; set; }
		public DateTime AppointmentDate { get; set; }
		public TimeSpan AppointmentStartTime { get; set; }
		public TimeSpan AppointmentEndTime { get; set; }
		public Guid UserId { get; set; }
		public Guid HairdresserServiceId { get; set; }
		public Guid HairdresserId { get; set; }
	}
}
