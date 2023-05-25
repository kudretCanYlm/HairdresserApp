namespace HairdresserService.Application.Dto
{
	public class HairdresserServiceDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public TimeSpan ServiceDuration { get; set; }
		public Guid HairdresserId { get; set; }
		public bool IsItActive { get; set; }
	}
}
