using NetDevPack.Messaging;

namespace Events.Hairdresser
{
	public class HairdresserCreatedEvent:Event
	{
		public HairdresserCreatedEvent()
		{

		}

		public HairdresserCreatedEvent(Guid id, string name, string? about, string address, string coordinate, bool isOpenNow, TimeSpan? openHour, TimeSpan? closeHour, string? workDays, Guid ownerId)
		{
			Id = id;
			AggregateId = id;
			Name = name;
			About = about;
			Address = address;
			Coordinate = coordinate;
			IsOpenNow = isOpenNow;
			OpenHour = openHour;
			CloseHour = closeHour;
			WorkDays = workDays;
			OwnerId = ownerId;
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
		public string? About { get; set; }
		public string Address { get; set; }
		public string Coordinate { get; set; }
		public bool IsOpenNow { get; set; } = true;
		public TimeSpan? OpenHour { get; set; }
		public TimeSpan? CloseHour { get; set; }
		//use enum like 1,2,3,4,5,6,7
		public string? WorkDays { get; set; }
		public Guid OwnerId { get; set; }
	}
}
