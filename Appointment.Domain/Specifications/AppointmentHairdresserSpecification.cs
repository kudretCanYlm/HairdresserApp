using Appointment.Domain.Models;
using Database.Specifications;
using System.Linq.Expressions;

namespace Appointment.Domain.Specifications
{
	public class AppointmentHairdresserSpecification : BaseSpecification<AppointmentModel>
	{
		public Guid HairdresserId { get; set; }

		public AppointmentHairdresserSpecification(Guid hairdresserId)
		{
			HairdresserId = hairdresserId;
		}

		public override Expression<Func<AppointmentModel, bool>> Criteria
			=> item => item.HairdresserId == HairdresserId;

	}
}
