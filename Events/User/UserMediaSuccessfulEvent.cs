using NetDevPack.Messaging;

namespace Events.User
{
	public class UserMediaSuccessfulEvent:Event, MassTransit.CorrelatedBy<Guid>
	{
		public UserMediaSuccessfulEvent(Guid correlationId, Guid id, Guid ownerId)
		{
			CorrelationId = correlationId;
			Id = id;
			OwnerId = ownerId;
		}

		public Guid Id { get; set; }
		public Guid OwnerId { get; set; }

		public Guid CorrelationId { get; }
	}
}
