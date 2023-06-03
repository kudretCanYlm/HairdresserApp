namespace Events.Media
{
	public class MediaDeletedRejectedEvent: MediaRejectedEvent
	{
		public MediaDeletedRejectedEvent(Guid correlationId, Guid id, Guid ownerId, string reason) : base(correlationId, id, ownerId, reason)
		{

		}
	}
}
