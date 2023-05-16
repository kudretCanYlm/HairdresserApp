using Database.Entity;
using NetDevPack.Domain;

namespace Hairdresser.Domain.Models
{
	public class HairdresserModel:BaseEntity,IAggregateRoot
	{
		public HairdresserModel()
		{
			Id = Guid.NewGuid();
		}

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
