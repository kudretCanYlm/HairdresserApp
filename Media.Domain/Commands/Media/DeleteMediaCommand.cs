using Media.Domain.Validations.MediaCommands;

namespace Media.Domain.Commands.Media
{
	public class DeleteMediaCommand:MediaCommand
	{
		public DeleteMediaCommand()
		{

		}
		public DeleteMediaCommand(Guid id)
		{
			Id = id;
		}

		public override bool IsValid()
		{
			ValidationResult = new DeleteMediaValidation().Validate(this);
			return ValidationResult.IsValid;
		}
	}
}
