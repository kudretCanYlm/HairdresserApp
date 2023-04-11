using NetDevPack.Messaging;

namespace Events.Auth
{
	public class UserRefreshedTokenEvent:Event
	{
		public UserRefreshedTokenEvent()
		{

		}

		public UserRefreshedTokenEvent(string token)
		{
			Token = token;
		}

		public string Token { get; set; }
	}
}
