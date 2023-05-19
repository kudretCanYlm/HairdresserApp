using Appointment.Domain.Commands.Appointment;

namespace Appointment.Domain.Validations.AppointmentCommands
{
	public class CancelAppointmentValidation:AppointmentValidation<CancelAppointmentCommand>
	{
		public CancelAppointmentValidation()
		{
			ValidateId();
			ValidateUserId();
		}
	}
}
