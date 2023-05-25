namespace HairdresserService.Domain.Commands.HairdresserService
{
	public class DeactivateHairdresserServiceCommand:HairdresserServiceCommand
	{
		public DeactivateHairdresserServiceCommand()
		{

		}

		public DeactivateHairdresserServiceCommand(Guid id, Guid hairdresserId)
		{
			Id = id;
			HairdresserId = hairdresserId;
			IsItActive = false;
		}
	}
}	
