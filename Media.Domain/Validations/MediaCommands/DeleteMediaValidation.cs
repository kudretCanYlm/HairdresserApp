using Media.Domain.Commands.Media;

namespace Media.Domain.Validations.MediaCommands
{
	public class DeleteMediaValidation:MediaValidation<DeleteMediaCommand>
	{
		public DeleteMediaValidation()
		{
			ValidateId();
		}
	}
}
