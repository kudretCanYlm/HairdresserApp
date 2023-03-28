using NetDevPack.Messaging;

namespace Events
{
	public interface IEventStore
	{
		void Save<T>(T @event) where T : Event;
	}
}
