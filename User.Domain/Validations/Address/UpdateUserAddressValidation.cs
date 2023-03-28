using User.Domain.Commands.Address;

namespace User.Domain.Validations.Address
{
	public class UpdateUserAddressValidation:UserAddressValidation<UpdateUserAddressCommand>
	{
		public UpdateUserAddressValidation()
		{
			ValidateId();
			ValidateStreet();
			ValidateCity();
			ValidateState();
			ValidateCountry();
			ValidateZipCode();
		}
	}
}
