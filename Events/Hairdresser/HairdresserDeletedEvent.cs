using NetDevPack.Messaging;

namespace Events.Hairdresser
{
	public class HairdresserDeletedEvent:Event
	{
		public HairdresserDeletedEvent(Guid id, Guid ownerId)
		{
			Id = id;
			AggregateId = id;
			OwnerId = ownerId;
		}

		public Guid Id { get; set; }
		public Guid OwnerId { get; set; }

	}
}
