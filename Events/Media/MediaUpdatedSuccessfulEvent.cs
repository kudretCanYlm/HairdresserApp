namespace Events.Media
{
	public class MediaUpdatedSuccessfulEvent: MediaSuccessfulEvent
	{
		public MediaUpdatedSuccessfulEvent(Guid correlationId, Guid id, Guid ownerId) : base(correlationId, id, ownerId)
		{

		}
	}
}
