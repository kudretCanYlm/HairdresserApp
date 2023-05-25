using Database.Entity;
using NetDevPack.Domain;

namespace HairdresserService.Domain.Models
{
	public class HairdresserServiceModel:BaseEntity,IAggregateRoot
	{
		public HairdresserServiceModel()
		{
			Id = Guid.NewGuid();
			IsItActive= true;
		}

		public string Name { get; set; }
		public decimal Price { get; set; }
		public TimeSpan ServiceDuration { get; set; }
		public Guid HairdresserId { get; set; }
		public bool IsItActive { get; set; } 
	}
}
