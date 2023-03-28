using Events.Stores.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Events.Stores.EfCore
{
	public static class EfCoreEventStoreExtensions
	{
		public static void AddEfCoreEventStore(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptions)
		{
			services.AddDbContext<EventStoreSqlContext>(dbContextOptions);
			services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
			services.AddScoped<IEventStore, EventStore>();

		}
	}
}
