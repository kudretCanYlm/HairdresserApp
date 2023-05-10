using Media.Domain.Validations.MediaCommands;

namespace Media.Domain.Commands.Media
{
	public class UpdateMediaCommand:MediaCommand
	{
		public UpdateMediaCommand()
		{

		}

		public UpdateMediaCommand(Guid id,string fileExtension, byte[] mediaData, string customType, Guid imageOwnerId)
		{
			Id = id;
			FileExtension = fileExtension;
			MediaData = mediaData;
			CustomType = customType;
			ImageOwnerId = imageOwnerId;
		}

		public override bool IsValid()
		{
			ValidationResult = new UpdateMediaValidation().Validate(this);
			return ValidationResult.IsValid;
		}
	}
}
