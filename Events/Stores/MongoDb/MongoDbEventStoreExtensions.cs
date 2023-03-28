using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Events.Stores.MongoDb.Repository;

namespace Events.Stores.MongoDb
{
	public static class MongoDbEventStoreExtensions
	{
		public static void AddMongoDbEventStore(this IServiceCollection services, IConfiguration Configuration)
		{
			var options = new MongoDbEventStoreOptions();
			Configuration.GetSection(nameof(MongoDbEventStoreOptions)).Bind(options);
			services.Configure<MongoDbEventStoreOptions>(Configuration.GetSection(nameof(MongoDbEventStoreOptions)));

			services.AddScoped<IEventStoreRepository, EventStoreMongoRepository>();
			services.AddScoped<IEventStore, EventStore>();

		}
	}
}
