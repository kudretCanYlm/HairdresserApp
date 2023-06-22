using Appointment.Domain.Models;
using MediatR;

namespace Appointment.Domain.Queries.Appointment
{
	public class GetApprovedAppointmentsByHairdresserIdQuery:IRequest<IEnumerable<AppointmentModel>>
	{
		public GetApprovedAppointmentsByHairdresserIdQuery(Guid hairdresserId)
		{
			HairdresserId = hairdresserId;
		}

		public Guid HairdresserId { get; set; }

	}
}
