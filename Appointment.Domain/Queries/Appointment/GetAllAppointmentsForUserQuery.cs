using Appointment.Domain.Models;
using MediatR;

namespace Appointment.Domain.Queries.Appointment
{
	public class GetAllAppointmentsForUserQuery:IRequest<IEnumerable<AppointmentModel>>
	{
		public GetAllAppointmentsForUserQuery(Guid hairdresserId, DateTime appointmentDate)
		{
			HairdresserId = hairdresserId;
			AppointmentDate = appointmentDate;
		}

		public Guid HairdresserId { get; set; }
		public DateTime AppointmentDate { get; set; }
	}
}
