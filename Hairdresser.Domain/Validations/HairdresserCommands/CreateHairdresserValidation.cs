using Hairdresser.Domain.Commands.Hairdresser;

namespace Hairdresser.Domain.Validations.HairdresserCommands
{
	public class CreateHairdresserValidation:HairdresserValidation<CreateHairdresserCommand>
	{
		public CreateHairdresserValidation()
		{
			ValidateName();
			ValidateAbout();
			ValidateAddress();
			ValidateCoordinate();
			ValidateOwnerId();
		}
	}
}
