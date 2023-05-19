using Events.Appointment.Enum;
using NetDevPack.Messaging;

namespace Events.Appointment
{
	public class AppointmentUpdatedEvent:Event
	{
		public AppointmentUpdatedEvent()
		{
			AppointmentState = AppointmentStateEnum.Updated;
		}

		public AppointmentUpdatedEvent(Guid id, string notes, DateTime appointmentDate, TimeSpan appointmentStartTime, Guid userId, Guid hairdresserServiceId)
		{
			AggregateId = id;
			AppointmentState = AppointmentStateEnum.Updated;
			Id = id;
			Notes = notes;
			AppointmentDate = appointmentDate;
			AppointmentStartTime = appointmentStartTime;
			UserId = userId;
			HairdresserServiceId = hairdresserServiceId;
		}

		public Guid Id { get; set; }
		public AppointmentStateEnum AppointmentState { get;private set; }
		public string Notes { get; set; }
		public DateTime AppointmentDate { get; set; }
		public TimeSpan AppointmentStartTime { get; set; }
		public Guid UserId { get; set; }
		public Guid HairdresserServiceId { get; set; }
	}
}
