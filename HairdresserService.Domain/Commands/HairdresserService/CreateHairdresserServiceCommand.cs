namespace HairdresserService.Domain.Commands.HairdresserService
{
	public class CreateHairdresserServiceCommand:HairdresserServiceCommand
	{
		public CreateHairdresserServiceCommand()
		{
			IsItActive= true;
		}

		public CreateHairdresserServiceCommand(string name,decimal price,TimeSpan serviceDuration,Guid hairdresserId)
		{
			IsItActive = true;
			Name = name;
			Price= price;
			ServiceDuration= serviceDuration;
			HairdresserId= hairdresserId;
		}
	}
}
