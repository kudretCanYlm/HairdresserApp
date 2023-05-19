using Events.Appointment.Enum;
using NetDevPack.Messaging;

namespace Events.Appointment
{
	public class AppointmentApprovedEvent:Event
	{
		public AppointmentApprovedEvent()
		{
			AppointmentState=AppointmentStateEnum.Approved;
		}

		public AppointmentApprovedEvent(Guid id, Guid hairdresserId)
		{
			AggregateId = id;
			AppointmentState = AppointmentStateEnum.Approved;
			Id = id;
			HairdresserId = hairdresserId;
		}

		public Guid Id { get; set; }
		public AppointmentStateEnum AppointmentState { get;private set; }
		public Guid HairdresserId { get; set; }
	}
}
