namespace Auth.Domain.Commands.Auth
{
	public class LogoutCommand:AuthCommand
	{
		public LogoutCommand(string userToken)
		{
			this.Token=userToken;
		}
	}
}
