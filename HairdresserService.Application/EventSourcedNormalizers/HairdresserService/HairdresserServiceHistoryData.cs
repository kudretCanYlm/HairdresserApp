namespace HairdresserService.Application.EventSourcedNormalizers.HairdresserService
{
	public class HairdresserServiceHistoryData
	{
		public string Action { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
		public decimal? Price { get; set; }
		public TimeSpan? ServiceDuration { get; set; }
		public string HairdresserId { get; set; }
		public bool? IsItActive { get; set; }
		public string Timestamp { get; set; }
		public string Who { get; set; }
	}
}
