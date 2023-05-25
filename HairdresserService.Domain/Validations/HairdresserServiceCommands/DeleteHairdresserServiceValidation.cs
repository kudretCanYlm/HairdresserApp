using HairdresserService.Domain.Commands.HairdresserService;

namespace HairdresserService.Domain.Validations.HairdresserServiceCommands
{
	public class DeleteHairdresserServiceValidation:HairdresserServiceValidation<DeleteHairdresserServiceCommand>
	{
		public DeleteHairdresserServiceValidation()
		{
			ValidateId();
			ValidateHairdresserId();
		}
	}
}
