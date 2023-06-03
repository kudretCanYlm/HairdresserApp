using Common.Media;

namespace HairdresserService.Application.Dto
{
	public class UpdateHairdresserServiceDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public TimeSpan ServiceDuration { get; set; }
		public Guid HairdresserId { get; set; }
		public Guid UserId { get; set; }
		public IEnumerable<MediaUpdateModel> Medias { get; set; }
	}
}
