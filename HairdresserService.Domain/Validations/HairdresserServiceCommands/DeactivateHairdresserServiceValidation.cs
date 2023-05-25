using HairdresserService.Domain.Commands.HairdresserService;

namespace HairdresserService.Domain.Validations.HairdresserServiceCommands
{
	public class DeactivateHairdresserServiceValidation:HairdresserServiceValidation<DeactivateHairdresserServiceCommand>
	{
		public DeactivateHairdresserServiceValidation()
		{
			ValidateId();
			ValidateHairdresserId();
		}
	}
}
