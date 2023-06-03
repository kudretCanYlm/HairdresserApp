using MediatR;

namespace Hairdresser.Domain.Queries.Hairdresser
{
	public class CheckHairdresserActiveQuery:IRequest<bool>
	{
		public CheckHairdresserActiveQuery(Guid id, DateTime appointmentDate, TimeSpan appointmentStartTime, TimeSpan serviceDuration)
		{
			Id = id;
			AppointmentDate = appointmentDate;
			AppointmentStartTime = appointmentStartTime;
			ServiceDuration = serviceDuration;
		}

		public Guid Id { get; set; }
		public DateTime AppointmentDate { get; set; }
		public TimeSpan AppointmentStartTime { get; set; }
		public TimeSpan ServiceDuration { get; set; }
	}
}
