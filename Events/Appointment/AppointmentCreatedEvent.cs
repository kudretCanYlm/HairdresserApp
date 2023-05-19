using Events.Appointment.Enum;
using NetDevPack.Messaging;

namespace Events.Appointment
{
	public class AppointmentCreatedEvent:Event
	{
		public AppointmentCreatedEvent()
		{
			AppointmentState = AppointmentStateEnum.Waiting;
		}

		public AppointmentCreatedEvent(Guid id,string notes, DateTime appointmentDate, TimeSpan appointmentStartTime, Guid userId, Guid hairdresserServiceId, Guid hairdresserId)
		{
			Id= id;
			AggregateId = id;
			AppointmentState = AppointmentStateEnum.Waiting;
			Notes = notes;
			AppointmentDate = appointmentDate;
			AppointmentStartTime = appointmentStartTime;
			UserId = userId;
			HairdresserServiceId = hairdresserServiceId;
			HairdresserId = hairdresserId;
		}

		public Guid Id { get; set; }
		public AppointmentStateEnum AppointmentState { get;private set; }
		public string Notes { get; set; }
		public DateTime AppointmentDate { get; set; }
		public TimeSpan AppointmentStartTime { get; set; }
		public Guid UserId { get; set; }
		public Guid HairdresserServiceId { get; set; }
		public Guid HairdresserId { get; set; }
	}
}
