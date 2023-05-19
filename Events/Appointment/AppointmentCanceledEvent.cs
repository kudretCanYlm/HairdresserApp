using Events.Appointment.Enum;
using NetDevPack.Messaging;

namespace Events.Appointment
{
	public class AppointmentCanceledEvent:Event
	{
		public AppointmentCanceledEvent()
		{
			AppointmentState = AppointmentStateEnum.Cancelled;
		}

		public AppointmentCanceledEvent(Guid id, Guid userId)
		{
			AggregateId = id;
			AppointmentState = AppointmentStateEnum.Cancelled;
			Id = id;
			UserId = userId;
		}

		public Guid Id { get; set; }
		public AppointmentStateEnum AppointmentState { get; private set; }
		public Guid UserId { get; set; }
	}
}
