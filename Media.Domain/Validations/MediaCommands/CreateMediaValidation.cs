using Media.Domain.Commands.Media;

namespace Media.Domain.Validations.MediaCommands
{
	public class CreateMediaValidation:MediaValidation<CreateMediaCommand>
	{
		public CreateMediaValidation()
		{
			ValidateFileExtension();
			ValidateMediaData();
			ValidateCustomType();
			ValidateImageOwnerId();
		}
	}
}
