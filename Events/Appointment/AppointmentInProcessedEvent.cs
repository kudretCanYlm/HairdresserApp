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

		public AppointmentInProcessedEvent(Guid id, Guid hairdresserId)
		{
			AggregateId = id;
			AppointmentState = AppointmentStateEnum.InProcess;
			Id = id;
			HairdresserId = hairdresserId;
		}

		public Guid Id { get; set; }
		public AppointmentStateEnum AppointmentState { get; private set; }
		public Guid HairdresserId { get; set; }
	}
}
