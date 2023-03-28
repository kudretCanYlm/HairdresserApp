using User.Domain.Validations.Address;

namespace User.Domain.Commands.Address
{
	public class UpdateUserAddressCommand:UserAddressCommand
	{
		public UpdateUserAddressCommand(Guid id, string street, string city, string state, string country, string zipCode)
		{
			Id = id;
			Street = street;
			City = city;
			State = state;
			Country = country;
			ZipCode = zipCode;
		}

		public override bool IsValid()
		{
			ValidationResult = new UpdateUserAddressValidation().Validate(this);
			return ValidationResult.IsValid;
		}
	}
}
