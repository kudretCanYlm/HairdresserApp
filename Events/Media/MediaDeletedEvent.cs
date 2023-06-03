using NetDevPack.Messaging;

namespace Events.Media
{
	public class MediaDeletedEvent:Event, MassTransit.CorrelatedBy<Guid>
	{
		public MediaDeletedEvent()
		{

		}
		public MediaDeletedEvent(Guid correlationId,Guid id, Guid imageOwnerId)
		{
			Id = id;
			AggregateId = id;
			ImageOwnerId = imageOwnerId;
			CorrelationId = correlationId;
		}

		public Guid Id { get; set; }
		public Guid ImageOwnerId { get; set; }
		public Guid CorrelationId { get; }
	}
}
