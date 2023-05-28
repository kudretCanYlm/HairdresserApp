namespace HairdresserService.Application.Dto
{
	public class CreateHairdresserServiceDto
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public TimeSpan ServiceDuration { get; set; }
		public Guid HairdresserId { get; set; }

		public IEnumerable<string> Base64MediaList { get; set; }
	}
}
