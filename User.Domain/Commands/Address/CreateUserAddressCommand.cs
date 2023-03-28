using User.Domain.Validations.Address;

namespace User.Domain.Commands.Address
{
	public class CreateUserAddressCommand:UserAddressCommand
	{
		public CreateUserAddressCommand(Guid userId, string street, string city, string state, string country, string zipCode)
		{
			UserId = userId;
			Street = street;
			City = city;
			State = state;
			Country = country;
			ZipCode = zipCode;
		}

		public override bool IsValid()
		{
			ValidationResult = new CreateUserAddressValidation().Validate(this);
			return ValidationResult.IsValid;
		}
	}
}
