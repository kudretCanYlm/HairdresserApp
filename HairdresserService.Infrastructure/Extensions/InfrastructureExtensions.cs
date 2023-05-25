using Database.Infrastructure;
using HairdresserService.Domain.Interfaces;
using HairdresserService.Infrastructure.Context;
using HairdresserService.Infrastructure.Repository.HairdresserService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HairdresserService.Infrastructure.Extensions
{
	public static class InfrastructureExtensions
	{
		public static void AddHairdresserServiceInfrastructure(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptions)
		{
			services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
			services.AddScoped(typeof(IDatabaseFactory<>), typeof(DatabaseFactory<>));

			services.AddScoped<IHairdresserServiceRepository, HairdresserServiceRepository>();
			services.AddDbContext<HairdresserServiceContext>(dbContextOptions);
		}
	}
}
