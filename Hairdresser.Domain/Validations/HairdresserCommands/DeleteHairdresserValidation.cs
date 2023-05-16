using Hairdresser.Domain.Commands.Hairdresser;

namespace Hairdresser.Domain.Validations.HairdresserCommands
{
	public class DeleteHairdresserValidation:HairdresserValidation<DeleteHairdresserCommand>
	{
		public DeleteHairdresserValidation()
		{
			ValidateId();
			ValidateOwnerId();
		}
	}
}
