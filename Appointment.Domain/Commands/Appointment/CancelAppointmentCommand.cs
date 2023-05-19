using Events.Appointment.Enum;

namespace Appointment.Domain.Commands.Appointment
{
	public class CancelAppointmentCommand : AppointmentCommand
	{
		public CancelAppointmentCommand()
		{
			AppointmentState = AppointmentStateEnum.Cancelled;
		}

		public CancelAppointmentCommand(Guid id, Guid userId)
		{
			AppointmentState = AppointmentStateEnum.Cancelled;
			Id = id;
			UserId = userId;
		}
	}
}
