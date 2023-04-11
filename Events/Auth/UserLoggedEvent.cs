using NetDevPack.Messaging;

namespace Events.Auth
{
	public class UserLoggedEvent:Event
	{
		public UserLoggedEvent(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; set; }
	}
}
