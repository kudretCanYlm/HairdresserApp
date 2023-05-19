using Appointment.Domain.Commands.Appointment;

namespace Appointment.Domain.Validations.AppointmentCommands
{
	public class UpdateAppointmentValidation:AppointmentValidation<UpdateAppointmentCommand>
	{
		public UpdateAppointmentValidation()
		{
			ValidateNotes();
			ValidateAppointmentDate();
			ValidateAppointmentStartTime();
			ValidateUserId();
			ValidateHairdresserServiceId();
			//ValidateHairdresserId();
		}
	}
}
