using HairdresserService.Domain.Commands.HairdresserService;

namespace HairdresserService.Domain.Validations.HairdresserServiceCommands
{
	public class UpdateHairdresserServiceValidation:HairdresserServiceValidation<UpdateHairdresserServiceCommand>
	{
		public UpdateHairdresserServiceValidation()
		{
			ValidateId();
			ValidateName();
			ValidatePrice();
			ValidateHairdresserId();
		}
	}
}
