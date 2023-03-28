using User.Domain.Validations.Address;

namespace User.Domain.Commands.Address
{
	public class DeleteUserAddressCommand:UserAddressCommand
	{
		public DeleteUserAddressCommand(Guid id)
		{
			Id = id;
		}

		public override bool IsValid()
		{
			ValidationResult = new DeleteUserAddressValidation().Validate(this);
			return ValidationResult.IsValid;
		}
	}
}
