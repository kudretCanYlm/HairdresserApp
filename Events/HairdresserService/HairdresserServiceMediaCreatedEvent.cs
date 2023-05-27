namespace Events.HairdresserService
{
	public class HairdresserServiceMediaCreatedEvent
	{
		public Guid Id { get; set; }
		public string FileExtension { get; set; }
		public byte[] MediaData { get; set; }
		public string CustomType { get; set; }
		public Guid ImageOwnerId { get; set; }
	}
}
