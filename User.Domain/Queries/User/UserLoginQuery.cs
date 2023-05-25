using MediatR;

namespace User.Domain.Queries.User
{
	public class UserLoginQuery:IRequest<Guid?>
	{
		public UserLoginQuery()
		{

		}

		public string Email { get; set; }
		public string Password { get; set; }
	}
}
