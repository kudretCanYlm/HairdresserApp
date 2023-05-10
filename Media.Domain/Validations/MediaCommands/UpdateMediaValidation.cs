using Media.Domain.Commands.Media;

namespace Media.Domain.Validations.MediaCommands
{
	public class UpdateMediaValidation:MediaValidation<UpdateMediaCommand>
	{
		public UpdateMediaValidation()
		{
			ValidateId();
			ValidateFileExtension();
			ValidateMediaData();
			ValidateCustomType();
			ValidateImageOwnerId();
		}
	}
}
