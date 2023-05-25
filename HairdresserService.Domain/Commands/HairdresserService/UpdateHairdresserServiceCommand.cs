namespace HairdresserService.Domain.Commands.HairdresserService
{
	public class UpdateHairdresserServiceCommand:HairdresserServiceCommand
	{
		public UpdateHairdresserServiceCommand()
		{

		}

		public UpdateHairdresserServiceCommand(Guid id,string name, decimal price, TimeSpan serviceDuration, Guid hairdresserId)
		{
			Id = id;
			Name = name;
			Price = price;
			ServiceDuration = serviceDuration;
			HairdresserId = hairdresserId;
		}
	}
}
