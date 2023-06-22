using Appointment.Domain.Models;
using MediatR;

namespace Appointment.Domain.Queries.Appointment
{
	public class GetAllAppointmentsByUserIdQuery:IRequest<IEnumerable<AppointmentModel>>
	{
		public GetAllAppointmentsByUserIdQuery(Guid userId)
		{
			UserId = userId;
		}

		public Guid UserId { get; set; }
	}
}
