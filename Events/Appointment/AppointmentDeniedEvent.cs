using Events.Appointment.Enum;
using NetDevPack.Messaging;

namespace Events.Appointment
{
	public class AppointmentDeniedEvent:Event
	{
		public AppointmentDeniedEvent()
		{
			AppointmentState = AppointmentStateEnum.Denied;
		}

		public AppointmentDeniedEvent(Guid id, Guid hairdresserId)
		{
			AppointmentState = AppointmentStateEnum.Denied;
			Id = id;
			HairdresserId = hairdresserId;
		}

		public Guid Id { get; set; }
		public AppointmentStateEnum AppointmentState { get; private set; }
		public Guid HairdresserId { get; set; }
	}
}
