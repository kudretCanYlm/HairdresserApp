using Appointment.Domain.Models;
using Database.Specifications;
using System.Linq.Expressions;

namespace Appointment.Domain.Specifications
{
	public class AppointmentEqualTimeSpecification : BaseSpecification<AppointmentModel>
	{
		private DateTime AppointmentDate { get; set; }

		public AppointmentEqualTimeSpecification(DateTime appointmentDate)
		{
			AppointmentDate = appointmentDate;
		}

		public override Expression<Func<AppointmentModel, bool>> Criteria => 
			item=>item.AppointmentEndTime>= AppointmentDate.TimeOfDay;
	}
}
