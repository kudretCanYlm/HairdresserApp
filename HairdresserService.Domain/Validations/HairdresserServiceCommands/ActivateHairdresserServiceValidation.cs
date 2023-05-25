using HairdresserService.Domain.Commands.HairdresserService;

namespace HairdresserService.Domain.Validations.HairdresserServiceCommands
{
	public class ActivateHairdresserServiceValidation: HairdresserServiceValidation<ActivateHairdresserServiceCommand>
	{
		public ActivateHairdresserServiceValidation()
		{
			ValidateId();
			ValidateHairdresserId();
		}
	}
}
