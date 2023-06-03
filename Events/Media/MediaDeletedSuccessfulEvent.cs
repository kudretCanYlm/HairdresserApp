namespace Events.Media
{
	public class MediaDeletedSuccessfulEvent: MediaSuccessfulEvent
	{
		public MediaDeletedSuccessfulEvent(Guid correlationId, Guid id, Guid ownerId) : base(correlationId, id, ownerId)
		{

		}
	}
}
