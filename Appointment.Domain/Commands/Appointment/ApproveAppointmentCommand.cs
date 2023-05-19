using Events.Appointment.Enum;

namespace Appointment.Domain.Commands.Appointment
{
	public class ApproveAppointmentCommand:AppointmentCommand
	{
		public ApproveAppointmentCommand()
		{
			AppointmentState = AppointmentStateEnum.Approved;
		}

		public ApproveAppointmentCommand(Guid id,Guid hairdresserId)
		{
			AppointmentState = AppointmentStateEnum.Approved;
			Id = id;
			HairdresserId= hairdresserId;
		}

	}
}
