using NetDevPack.Messaging;

namespace Events.HairdresserService
{
	public class HairdresserServiceDeactivatedEvent:Event
	{
		public HairdresserServiceDeactivatedEvent()
		{

		}

		public HairdresserServiceDeactivatedEvent(Guid id, Guid hairdresserId)
		{
			AggregateId = id;
			Id = id;
			HairdresserId = hairdresserId;
			IsItActive = false;
		}

		public Guid Id { get; set; }
		public Guid HairdresserId { get; set; }
		public bool IsItActive { get; private set; }
	}
}
