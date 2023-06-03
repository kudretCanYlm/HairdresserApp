using Appointment.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
