using NetDevPack.Messaging;

namespace Events.User
{
	public class UserDeletedEvent:Event
	{
		public UserDeletedEvent()
		{

		}

		public UserDeletedEvent(Guid id)
		{
			Id = id;
			AggregateId= id;
		}

		public Guid Id { get; set; }
		
	}
}
