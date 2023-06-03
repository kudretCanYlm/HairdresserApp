namespace HairdresserService.Application.Dto
{
	public class CreateHairdresserServiceMediaDto
	{
		public Guid Id { get; set; }
		public Guid HairdresserId { get; set; }
		public Guid UserId { get; set; }
		public IEnumerable<string> Medias { get; set; }

	}
}
