using Appointment.Domain.Models;
using Database.Specifications;
using Events.Appointment.Enum;
using System.Linq.Expressions;

namespace Appointment.Domain.Specifications
{
	public class AppointmentSingleStateSpecification : BaseSpecification<AppointmentModel>
	{

		private AppointmentStateEnum AppointmentState;

		public AppointmentSingleStateSpecification(AppointmentStateEnum appointmentState)
		{
			AppointmentState = appointmentState;
		}

		public override Expression<Func<AppointmentModel, bool>> Criteria =>
			item => item.AppointmentState == AppointmentState;
	}
}
