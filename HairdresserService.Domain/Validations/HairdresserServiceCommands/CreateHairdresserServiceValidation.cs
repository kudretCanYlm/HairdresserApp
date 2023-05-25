using HairdresserService.Domain.Commands.HairdresserService;

namespace HairdresserService.Domain.Validations.HairdresserServiceCommands
{
	public class CreateHairdresserServiceValidation:HairdresserServiceValidation<CreateHairdresserServiceCommand>
	{
		public CreateHairdresserServiceValidation()
		{
			ValidateName();
			ValidatePrice();
			ValidateHairdresserId();
		}
	}
}
