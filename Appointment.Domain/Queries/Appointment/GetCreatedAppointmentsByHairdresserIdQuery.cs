using Appointment.Domain.Models;
using MediatR;

namespace Appointment.Domain.Queries.Appointment
{
	public class GetCreatedAppointmentsByHairdresserIdQuery:IRequest<IEnumerable<AppointmentModel>>
	{
		public GetCreatedAppointmentsByHairdresserIdQuery(Guid hairdresserId)
		{
			HairdresserId = hairdresserId;
		}

		public Guid HairdresserId { get; set; }
	}
}
