namespace Events.Media
{
	public class MediaUpdatedRejectedEvent: MediaRejectedEvent
	{
		public MediaUpdatedRejectedEvent(Guid correlationId, Guid id, Guid ownerId, string reason) : base(correlationId, id, ownerId, reason)
		{

		}
	}
}
