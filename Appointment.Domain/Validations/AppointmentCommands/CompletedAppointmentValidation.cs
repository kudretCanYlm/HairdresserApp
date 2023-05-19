using Appointment.Domain.Commands.Appointment;

namespace Appointment.Domain.Validations.AppointmentCommands
{
	public class CompletedAppointmentValidation:AppointmentValidation<CompleteAppointmentCommand>
	{
		public CompletedAppointmentValidation()
		{
			ValidateId();
			ValidateHairdresserId();
		}
	}
}
