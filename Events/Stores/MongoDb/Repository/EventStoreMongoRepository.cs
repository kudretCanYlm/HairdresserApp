using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Events.Stores.MongoDb.Repository
{
	public class EventStoreMongoRepository : IEventStoreRepository
	{
		private readonly IMongoCollection<StoredEvent> storedEvents;

		public EventStoreMongoRepository(IOptions<MongoDbEventStoreOptions> options)
		{
			var client = new MongoClient(options.Value.ConnectionString);
			var database = client.GetDatabase(options.Value.DatabaseName);
			storedEvents = CreateOrGetCollection(database, options.Value.CollectionName);
		}
		private IMongoCollection<StoredEvent> CreateOrGetCollection(IMongoDatabase database, string collectionName)
		{
			var filter = new BsonDocument("name", collectionName);
			var collections = database.ListCollections(new ListCollectionsOptions { Filter = filter });

			var doesCollectionExist = collections.Any();

			if (!doesCollectionExist)
			{
				database.CreateCollection(collectionName);
			}

			return database.GetCollection<StoredEvent>(collectionName);
		}

		public async Task<IList<StoredEvent>> All(Guid aggregateId)
		{
			var builder = Builders<StoredEvent>.Filter;
			var filter = builder.Where(d => d.AggregateId == aggregateId);
			var result = await storedEvents.Find(filter).ToListAsync();

			return result;
		}

		public void Dispose()
		{
			
		}

		public void Store(StoredEvent @event)
		{
			 storedEvents.InsertOne(@event);
		}
	}
}
