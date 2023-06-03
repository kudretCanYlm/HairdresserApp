	namespace Events.Media
{
	public class MediaCreatedSuccessfulEvent:MediaSuccessfulEvent
	{
		public MediaCreatedSuccessfulEvent(Guid correlationId, Guid id, Guid ownerId) :base(correlationId,id,ownerId)
		{

		}
	}
}
