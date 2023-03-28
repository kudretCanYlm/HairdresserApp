using User.Domain.Commands.Address;

namespace User.Domain.Validations.Address
{
	public class DeleteUserAddressValidation:UserAddressValidation<DeleteUserAddressCommand>
	{
		public DeleteUserAddressValidation()
		{
			ValidateId();
		}
	}
}
