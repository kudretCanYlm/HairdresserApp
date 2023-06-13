using Appointment.Domain.Models;
using Database.Specifications;
using System.Linq.Expressions;

namespace Appointment.Domain.Specifications
{
	public class AppointmentTimeConflictSpecification : BaseSpecification<AppointmentModel>
	{
		public DateTime AppointmentDate { get; set; }
		public TimeSpan AppointmentStartTime { get; set; }
		public TimeSpan ServiceDuration { get; set; }

		public AppointmentTimeConflictSpecification(DateTime appointmentDate, TimeSpan appointmentStartTime, TimeSpan serviceDuration)
		{
			AppointmentDate = appointmentDate;
			AppointmentStartTime = appointmentStartTime;
			ServiceDuration = serviceDuration;
		}

		public override Expression<Func<AppointmentModel, bool>> Criteria
				=> item =>(
				item.AppointmentDate.Date == AppointmentDate.Date
				&&
				(
					   (
					   AppointmentStartTime >= item.AppointmentStartTime
					   &&
					   AppointmentStartTime <= item.AppointmentEndTime
					   )
					||
					   (
					   AppointmentStartTime.Add(ServiceDuration) >= item.AppointmentStartTime
					   &&
					   AppointmentStartTime.Add(ServiceDuration) <= item.AppointmentEndTime
					   )
				));
	}
}
