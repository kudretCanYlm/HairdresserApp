using User.Domain.Commands.Address;

namespace User.Domain.Validations.Address
{
	public class CreateUserAddressValidation: UserAddressValidation<UserAddressCommand>
	{
		public CreateUserAddressValidation()
		{
			ValidateUserId();
			ValidateStreet();
			ValidateCity();
			ValidateState();
			ValidateCountry();
			ValidateZipCode();
		}
	}
}
