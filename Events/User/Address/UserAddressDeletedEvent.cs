using NetDevPack.Messaging;

namespace Events.User.Address
{
	public class UserAddressDeletedEvent:Event
	{
		public UserAddressDeletedEvent()
		{

		}

		public UserAddressDeletedEvent(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; set; }
	}
}
