using Events.Appointment.Enum;
using NetDevPack.Messaging;

namespace Events.Appointment
{
	public class AppointmentCompletedEvent:Event
	{

		public AppointmentCompletedEvent()
		{
			AppointmentState = AppointmentStateEnum.Completed;
		}

		public AppointmentCompletedEvent(Guid id, Guid hairdresserId, Guid userId)
		{
			AggregateId = id;
			AppointmentState = AppointmentStateEnum.Completed;
			Id = id;
			HairdresserId = hairdresserId;
			UserId = userId;
		}

		public Guid Id { get; set; }
		public AppointmentStateEnum AppointmentState { get; private set; }
		public Guid HairdresserId { get; set; }
		public Guid UserId { get; set; }
	}
}
