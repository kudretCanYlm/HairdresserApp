using NetDevPack.Messaging;

namespace Events.HairdresserService
{
	public class HairdresserServiceCreatedEvent:Event
	{
		public HairdresserServiceCreatedEvent()
		{

		}

		public HairdresserServiceCreatedEvent(Guid id, string name, decimal price, TimeSpan serviceDuration, Guid hairdresserId)
		{
			AggregateId = id;
			Id = id;
			Name = name;
			Price = price;
			ServiceDuration = serviceDuration;
			HairdresserId = hairdresserId;
			IsItActive = true;
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public TimeSpan ServiceDuration { get; set; }
		public Guid HairdresserId { get; set; }
		public bool IsItActive { get; set; }
	}
}
