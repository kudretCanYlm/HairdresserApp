using Appointment.Domain.Models;
using Database.Specifications;
using Events.Appointment.Enum;
using System.Linq.Expressions;

namespace Appointment.Domain.Specifications
{
	public class AppointmentStateCanceledOrDeniedSpecification: BaseSpecification<AppointmentModel>
	{

		public override Expression<Func<AppointmentModel, bool>> Criteria
			=> item => (
				item.AppointmentState != AppointmentStateEnum.Denied
				||
				item.AppointmentState != AppointmentStateEnum.Cancelled
				);

	}
}
