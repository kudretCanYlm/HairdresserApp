using Events.Appointment.Enum;

namespace Appointment.Domain.Commands.Appointment
{
	public class InProcessAppointmentCommand:AppointmentCommand
	{
		public InProcessAppointmentCommand()
		{
			AppointmentState = AppointmentStateEnum.InProcess;
		}

		public InProcessAppointmentCommand(Guid id, Guid hairdresserId)
		{
			AppointmentState = AppointmentStateEnum.InProcess;
			Id = id;
			HairdresserId = hairdresserId;
		}
	}
}
