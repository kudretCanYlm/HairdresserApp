using Events.Appointment.Enum;
using NetDevPack.Messaging;

namespace Events.Appointment
{
	public class AppointmentInProcessedEvent:Event
	{

		public AppointmentInProcessedEvent()
		{
			AppointmentState = AppointmentStateEnum.InProcess;
		}

		public AppointmentInProcessedEvent(Guid id, Guid hairdresserId, Guid userId)
		{
			AggregateId = id;
			AppointmentState = AppointmentStateEnum.InProcess;
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
