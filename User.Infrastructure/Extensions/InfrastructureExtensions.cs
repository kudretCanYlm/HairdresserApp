using Database.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using User.Domain.Interfaces;
using User.Infrastructure.Context;
using User.Infrastructure.Repository.Address;
using User.Infrastructure.Repository.User;

namespace User.Infrastructure.Extensions
{
	public static class InfrastructureExtensions
	{
		public static void UseUserInfrastructure(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptions)
		{
			services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
			services.AddScoped(typeof(IDatabaseFactory<>), typeof(DatabaseFactory<>));

			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IAddressRepository, AddressRepository>();
			services.AddDbContext<UserContext>(dbContextOptions);

		}
	}
}
