using FluentValidation.Results;
using User.Application.EventSourcedNormalizers.User;
using User.Application.Dto.User;

namespace User.Application.Interfaces.User
{
	public interface IUserAppService:IDisposable
	{
		Task<Guid?> Login(LoginDto loginDto);
		Task<IEnumerable<UserDto>> GetAllAsync();
		Task<UserDto> GetByIdAsync(Guid id);
		Task<ValidationResult> CreateAsync(CreateUserDto createUserDto);
		Task<ValidationResult> UpdateAsync(UpdateUserDto updateUserDto);
		Task<ValidationResult> RemoveAsync(Guid id);
		Task<IList<UserHistoryData>> GetAllHistoryAsync(Guid id);
	}
}
