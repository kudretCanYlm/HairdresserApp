using Auth.Domain.Models;
using MediatR;

namespace Auth.Domain.Commands.Auth
{
	public class RefreshTokenCommand:IRequest<AuthSessionModel>
	{
		public RefreshTokenCommand(string userToken)
		{
			this.Token= userToken;
		}

		public string Token { get; set; }
	}
}
