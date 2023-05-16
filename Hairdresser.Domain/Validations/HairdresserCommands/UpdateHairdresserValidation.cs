using Hairdresser.Domain.Commands.Hairdresser;

namespace Hairdresser.Domain.Validations.HairdresserCommands
{
	public class UpdateHairdresserValidation:HairdresserValidation<UpdateHairdresserCommand>
	{
		public UpdateHairdresserValidation()
		{
			ValidateId();
			ValidateName();
			ValidateAbout();
			ValidateAddress();
			ValidateCoordinate();
			ValidateOwnerId();
		}
	}
}
