using Database.Infrastructure;
using Hairdresser.Domain.Interfaces;
using Hairdresser.Infrastructure.Context;
using Hairdresser.Infrastructure.Repository.Hairdresser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hairdresser.Infrastructure.Extensions
{
	public static class InfrastructureExtensions
	{
		public static void UseHairdresserInfrastructure(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptions)
		{
			services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
			services.AddScoped(typeof(IDatabaseFactory<>), typeof(DatabaseFactory<>));

			services.AddScoped<IHairdresserRepository, HairdresserRepository>();
			services.AddDbContext<HairdresserContext>(dbContextOptions);
		}
	}
}
