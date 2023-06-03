using Appointment.Domain.Models;
using MediatR;

namespace Appointment.Domain.Queries.Appointment
{
	public class GetAppointmentByIdAndUserIdQuery:IRequest<AppointmentModel>
	{
		public GetAppointmentByIdAndUserIdQuery(Guid id, Guid userId)
		{
			Id = id;
			UserId = userId;
		}

		public Guid Id { get; set; }
		public Guid UserId { get; set; }
	}
}
