using Auth.Domain.Models;
using MediatR;

namespace Auth.Domain.Commands.Auth
{
	public class LoginCommand: IRequest<AuthSessionModel>
	{
		public LoginCommand(Guid userId)
		{
			this.TokenOwnerId = userId;
		}
	
		public Guid TokenOwnerId { get; set; }
	}
}
