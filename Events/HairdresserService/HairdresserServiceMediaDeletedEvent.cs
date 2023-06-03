namespace Events.HairdresserService
{
	public class HairdresserServiceMediaDeletedEvent
	{
		public Guid Id { get; set; }
		public Guid ImageOwnerId { get; set; }
	}
}
