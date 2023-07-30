using Auth.Domain.Models;

namespace Auth.Domain.Interfaces.Auth
{
    public interface IAuthRepository
    {
        Task<AuthSessionModel> CreateTokenAsync(Guid UserId);
        Task<bool> DeleteTokenAsync(string token);
        Task<bool> DeleteTokenByIdAndUserIdAsync(Guid id, Guid userId);
		Task<AuthSessionModel> RefreshTokenAsync(string token);
        Task<IEnumerable<AuthSessionModel>> GetAllTokensAsync(Guid tokenOwner);
    }
}
