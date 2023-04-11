using Auth.Domain.Interfaces.Auth;
using Auth.Infrastructure.Repository.Auth;
using Database.Repository.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure.Extensions
{
	public static class InfrastructureExtensions
	{
		public static void UseAuthInfrastructure(this IServiceCollection services)
		{
			services.AddScoped(typeof(IRedisBaseRepository<>), typeof(RedisBaseRepository<>));
			services.AddScoped<IAuthRepository, AuthRepository>();
		}
	}
}
