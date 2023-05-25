namespace HairdresserService.Domain.Commands.HairdresserService
{
	public class ActivateHairdresserServiceCommand:HairdresserServiceCommand
	{
		public ActivateHairdresserServiceCommand()
		{

		}

		public ActivateHairdresserServiceCommand(Guid id,Guid hairdresserId)
		{
			Id = id;
			HairdresserId= hairdresserId;
			IsItActive= true;
		}
	}
}
