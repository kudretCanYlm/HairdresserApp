namespace HairdresserService.Domain.Commands.HairdresserService
{
	public class DeleteHairdresserServiceCommand:HairdresserServiceCommand
	{
		public DeleteHairdresserServiceCommand()
		{

		}

		public DeleteHairdresserServiceCommand(Guid id, Guid hairdresserId)
		{
			Id = id;
			HairdresserId = hairdresserId;
		}
	}
}
