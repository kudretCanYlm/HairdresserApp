using Appointment.Domain.Commands.Appointment;

namespace Appointment.Domain.Validations.AppointmentCommands
{
	public class InProcessAppointmentValidation:AppointmentValidation<InProcessAppointmentCommand>
	{
		public InProcessAppointmentValidation()
		{
			ValidateId();
			ValidateHairdresserId();
		}
	}
}
