using Appointment.Domain.Commands.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Domain.Validations.AppointmentCommands
{
	public class DenyAppointmentValidation:AppointmentValidation<DenyAppointmentCommand>
	{
		public DenyAppointmentValidation()
		{
			ValidateId();
			ValidateHairdresserId();
		}
	}
}
