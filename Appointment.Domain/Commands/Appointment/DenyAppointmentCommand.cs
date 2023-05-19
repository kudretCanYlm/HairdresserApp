using Events.Appointment.Enum;

namespace Appointment.Domain.Commands.Appointment
{
	public class DenyAppointmentCommand:AppointmentCommand
	{
		public DenyAppointmentCommand()
		{
			AppointmentState = AppointmentStateEnum.Denied;
		}

		public DenyAppointmentCommand(Guid id,Guid hairdresserId)
		{
			AppointmentState = AppointmentStateEnum.Denied;
			Id = id;
			HairdresserId= hairdresserId;
		}
	}
}
