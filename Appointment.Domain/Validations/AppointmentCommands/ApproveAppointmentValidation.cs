using Appointment.Domain.Commands.Appointment;

namespace Appointment.Domain.Validations.AppointmentCommands
{
	public class ApproveAppointmentValidation:AppointmentValidation<ApproveAppointmentCommand>
	{
		public ApproveAppointmentValidation()
		{
			ValidateId();
			ValidateHairdresserId();
		}
	}
}
