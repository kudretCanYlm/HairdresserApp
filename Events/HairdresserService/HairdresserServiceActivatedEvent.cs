using NetDevPack.Messaging;

namespace Events.HairdresserService
{
	public class HairdresserServiceActivatedEvent:Event
	{
		public HairdresserServiceActivatedEvent()
		{
			
		}

		public HairdresserServiceActivatedEvent(Guid id, Guid hairdresserId)
		{
			AggregateId= id;
			Id = id;
			HairdresserId = hairdresserId;
			IsItActive = true;
		}

		public Guid Id { get; set; }
		public Guid HairdresserId { get; set; }
		public bool IsItActive { get;private set; }
	}
}
