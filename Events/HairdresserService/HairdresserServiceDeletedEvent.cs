using NetDevPack.Messaging;

namespace Events.HairdresserService
{
	public class HairdresserServiceDeletedEvent:Event
	{
		public HairdresserServiceDeletedEvent()
		{

		}
		public HairdresserServiceDeletedEvent(Guid id, Guid hairdresserId)
		{
			AggregateId = id;
			Id = id;
			HairdresserId = hairdresserId;
		}

		public Guid Id { get; set; }
		public Guid HairdresserId { get; set; }

	}
}
