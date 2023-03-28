using NetDevPack.Identity.User;
using NetDevPack.Messaging;
using Newtonsoft.Json;

namespace Events.Stores
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        //private readonly IAspNetUser _user;

        public EventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public void Save<T>(T theEvent) where T : Event
        {
            // Using Newtonsoft.Json because System.Text.Json
            // is a sad joke to be considered "Done"

            // The System.Text don't know how serialize a
            // object with inherited properties, I said is sad...
            // Yes! I tried: options = new JsonSerializerOptions { WriteIndented = true };

            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                //_user.Name ?? _user.GetUserEmail()
                "Test Name"
                );

            _eventStoreRepository.Store(storedEvent);
        }
    }
}
