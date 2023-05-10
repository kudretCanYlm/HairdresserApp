using Media.Domain.Validations.MediaCommands;

namespace Media.Domain.Commands.Media
{
	public class CreateMediaCommand : MediaCommand
	{
		public CreateMediaCommand()
		{

		}
		public CreateMediaCommand(string fileExtension, byte[] mediaData,string customType,Guid imageOwnerId)
		{
			FileExtension = fileExtension;
			MediaData = mediaData;
			CustomType = customType;
			ImageOwnerId = imageOwnerId;
		}

		public override bool IsValid()
		{
			ValidationResult = new CreateMediaValidation().Validate(this);
			return ValidationResult.IsValid;
		}
	}
}
