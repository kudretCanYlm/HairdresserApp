using Appointment.Domain.Models;
using Database.Specifications;
using System.Linq.Expressions;

namespace Appointment.Domain.Specifications
{
	public class AppointmentEqualDateSpecification : BaseSpecification<AppointmentModel>
	{
		public DateTime AppointmentDate { get; set; }

		public AppointmentEqualDateSpecification(DateTime appointmentDate)
		{
			AppointmentDate = appointmentDate;
		}

		public override Expression<Func<AppointmentModel, bool>> Criteria
			=> item=>item.AppointmentDate.Date == AppointmentDate.Date;

	}
}
