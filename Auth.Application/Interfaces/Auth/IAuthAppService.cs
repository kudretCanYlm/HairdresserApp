using Auth.Application.Dto.Auth;
using FluentValidation.Results;

namespace Auth.Application.Interfaces.Auth
{
	public interface IAuthAppService
	{
		Task<UserAuthSessionDto> CheckTokenAndAddExpiring(string token);
		Task<UserAuthSessionDto> CreateToken(Guid userId);
		Task<ValidationResult> DeleteToken(string token);
		Task<IEnumerable<UserAuthSessionDto>> GetAllTokens(Guid userId);
	}
}
