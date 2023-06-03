using Appointment.Domain.Models;
using MediatR;

namespace Appointment.Domain.Queries.Appointment
{
	public class GetAppointmentByIdQuery:IRequest<AppointmentModel>
	{
		public GetAppointmentByIdQuery(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; set; }
	}
}
