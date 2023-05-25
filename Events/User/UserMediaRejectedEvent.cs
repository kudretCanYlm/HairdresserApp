using NetDevPack.Messaging;

namespace Events.User
{
	public class UserMediaRejectedEvent:Event,MassTransit.CorrelatedBy<Guid>
	{
		public UserMediaRejectedEvent(Guid correlationId, Guid id, Guid ownerId, string reason)
		{
			CorrelationId = correlationId;
			Id = id;
			OwnerId = ownerId;
			Reason = reason;
		}


		public Guid Id { get; set; }
		public Guid OwnerId { get; set; }
		public string Reason { get; set; }

		public Guid CorrelationId { get; }
	}
}
