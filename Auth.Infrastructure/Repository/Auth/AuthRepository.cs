using Auth.Domain.Interfaces.Auth;
using Auth.Domain.Models;
using Database.Repository.Redis;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using ZstdSharp.Unsafe;

namespace Auth.Infrastructure.Repository.Auth
{
	public class AuthRepository : IAuthRepository
	{

		private readonly IRedisBaseRepository<AuthSessionModel> _redisBaseRepository;
		private readonly ILogger<AuthRepository> _logger;
		public AuthRepository(IRedisBaseRepository<AuthSessionModel> redisBaseRepository, ILogger<AuthRepository> logger)
		{
			_redisBaseRepository = redisBaseRepository;
			_logger = logger;
		}

		public async Task<AuthSessionModel> CreateTokenAsync(Guid UserId)
		{
			var authModel = new AuthSessionModel(UserId, DateTime.Now.AddDays(30));
			var result = await _redisBaseRepository.InsertAsync(authModel.GenerateToken());
			await _redisBaseRepository.AddExpire(result, TimeSpan.FromDays(30));

			return authModel;
		}

		public async Task<bool> DeleteTokenAsync(string token)
		{

			var model = await _redisBaseRepository.GetSingle(x => x.Token == token);

			if (model == null)
				return false;

			try
			{
				await _redisBaseRepository.DeleteAsync(model);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError($"token didn't delete : ${token} ,Error :${ex.Message}");
				return false;
			}
		}

		public async Task<bool> DeleteTokenByIdAndUserIdAsync(Guid id, Guid userId)
		{
			var model=await _redisBaseRepository.GetSingle(x=>x.Id==id && x.TokenOwnerId==userId);

			if (model == null)
				return false;

			try
			{
				await _redisBaseRepository.DeleteAsync(model);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<IEnumerable<AuthSessionModel>> GetAllTokensAsync(Guid tokenOwner)
		{
			var models = await _redisBaseRepository.GetAllByWhereAsync(x => x.TokenOwnerId == tokenOwner);

			return models;
		}

		public async Task<AuthSessionModel> RefreshTokenAsync(string token)
		{
			var model = await _redisBaseRepository.GetSingle(x => x.Token == token);
			if (model == null)
				return null;
			model.TokenExpiringTime = DateTime.Now.AddDays(30);
			await _redisBaseRepository.UpdateAsync(model);
			await _redisBaseRepository.AddExpire(nameof(AuthSessionModel) + ":" + model.Id, TimeSpan.FromDays(30));

			return model;

		}

		public void Dispose()
		{
			_redisBaseRepository.Dispose();
		}
	}
}
