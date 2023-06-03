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

		public AppointmentApprovedEvent(Guid id, Guid hairdresserId, Guid userId, Guid hairdresserServiceId)
		{
			AggregateId = id;
			AppointmentState = AppointmentStateEnum.Approved;
			Id = id;
			HairdresserId = hairdresserId;
			UserId = userId;
			HairdresserServiceId = hairdresserServiceId;
		}

		public Guid Id { get; set; }
		public AppointmentStateEnum AppointmentState { get;private set; }
		public Guid HairdresserId { get; set; }
		public Guid UserId { get; set; }
		public Guid HairdresserServiceId { get; set; }
	}
}
