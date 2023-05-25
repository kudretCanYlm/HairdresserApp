using NetDevPack.Messaging;

namespace Events.HairdresserService
{
	public class HairdresserServiceUpdatedEvent:Event
	{
		public HairdresserServiceUpdatedEvent(Guid id, string name, decimal price, TimeSpan serviceDuration, Guid hairdresserId)
		{
			AggregateId = id;
			Id = id;
			Name = name;
			Price = price;
			ServiceDuration = serviceDuration;
			HairdresserId = hairdresserId;
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public TimeSpan ServiceDuration { get; set; }
		public Guid HairdresserId { get; set; }
	}
}
