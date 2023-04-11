using NetDevPack.Messaging;

namespace Events.Auth
{
	public class UserLoggedOutEvent:Event
	{
		public UserLoggedOutEvent()
		{

		}

		public UserLoggedOutEvent(string token)
		{
			Token = token;
		}

		public string Token { get; set; }
	}
}
