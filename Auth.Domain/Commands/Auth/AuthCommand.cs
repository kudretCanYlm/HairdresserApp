using NetDevPack.Messaging;

namespace Auth.Domain.Commands.Auth
{
	public class AuthCommand:Command
	{
		public string Token { get; set; }
		public Guid TokenOwnerId { get; set; }
		public DateTime TokenExpiringTime { get; set; }
	}
}
