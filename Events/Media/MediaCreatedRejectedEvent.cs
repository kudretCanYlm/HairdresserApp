namespace Events.Media
{
	public class MediaCreatedRejectedEvent:MediaRejectedEvent
	{
		public MediaCreatedRejectedEvent(Guid correlationId, Guid id, Guid ownerId, string reason) :base(correlationId,id,ownerId,reason)
		{

		}
	}
}
