using Events.Appointment.Enum;

namespace Appointment.Domain.Commands.Appointment
{
	public class UpdateAppointmentCommand : AppointmentCommand
	{
		public UpdateAppointmentCommand()
		{
			AppointmentState = AppointmentStateEnum.Updated;
		}

		public UpdateAppointmentCommand(Guid id, string notes, DateTime appointmentDate, TimeSpan appointmentStartTime, Guid userId, Guid hairdresserServiceId)
		{
			AppointmentState = AppointmentStateEnum.Updated;
			Id = id;
			Notes = notes;
			AppointmentDate = appointmentDate;
			AppointmentStartTime = appointmentStartTime;
			UserId = userId;
			HairdresserServiceId = hairdresserServiceId;
		}
	}
}
