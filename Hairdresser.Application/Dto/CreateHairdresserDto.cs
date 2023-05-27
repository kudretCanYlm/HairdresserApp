namespace Hairdresser.Application.Dto
{
	public class CreateHairdresserDto
	{
		public string Name { get; set; }
		public string? About { get; set; }
		public string Address { get; set; }
		public string Coordinate { get; set; }
		public TimeSpan? OpenHour { get; set; }
		public TimeSpan? CloseHour { get; set; }
		public string? WorkDays { get; set; }
		public Guid OwnerId { get; set; }
		public string Base64Media { get; set; }
	}
}
