using FluentValidation.Results;
using User.Application.Dto.Address;
using User.Application.Dto.User;
using User.Application.EventSourcedNormalizers.Address;

namespace User.Application.Interfaces.Address
{
	public interface IAddressAppService
	{
		Task<IEnumerable<AddressDto>> GetAll();
		Task<IEnumerable<AddressDto>> GetAllByUserId(Guid userId);
		Task<UserDto> GetById(Guid id);
		Task<ValidationResult> Create(CreateAddressDto createUserAddressDto);
		Task<ValidationResult> Update(UpdateAddressDto updateUserAddressDto);
		Task<ValidationResult> Remove(Guid id);
		Task<IList<AddressHistoryData>> GetAllHistory(Guid id);
	}
}
