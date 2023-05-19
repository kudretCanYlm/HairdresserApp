using Appointment.Domain.Commands.Appointment;

namespace Appointment.Domain.Validations.AppointmentCommands
{
	public class CreateAppointmentValidation:AppointmentValidation<CreateAppointmentCommand>
	{
		public CreateAppointmentValidation()
		{
			ValidateNotes();
			ValidateAppointmentDate();
			ValidateAppointmentStartTime();
			ValidateUserId();
			ValidateHairdresserServiceId();
			ValidateHairdresserId();
		}
	}
}
