using Appointment.Domain.Models;
using MediatR;

namespace Appointment.Domain.Queries.Appointment
{
	public class GetAllAppointmentsByHairdresserIdQuery:IRequest<IEnumerable<AppointmentModel>>
	{
		public GetAllAppointmentsByHairdresserIdQuery(Guid hairdresserId)
		{
			HairdresserId = hairdresserId;
		}

		public Guid HairdresserId { get; set; }
	}
}
