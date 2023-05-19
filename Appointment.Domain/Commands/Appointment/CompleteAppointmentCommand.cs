using Events.Appointment.Enum;

namespace Appointment.Domain.Commands.Appointment
{
	public class CompleteAppointmentCommand:AppointmentCommand
	{
		public CompleteAppointmentCommand()
		{
			AppointmentState = AppointmentStateEnum.Completed;
		}

		public CompleteAppointmentCommand(Guid id, Guid hairdresserId)
		{
			AppointmentState = AppointmentStateEnum.Completed;
			Id = id;
			HairdresserId = hairdresserId;
		}
	}
}
