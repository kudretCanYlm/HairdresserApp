using MediatR;
using NetDevPack.Messaging;
using User.Domain.Models;

namespace User.Domain.Queries.User
{
	public class GetUserByIdQuery:IRequest<UserModel>
	{
		public GetUserByIdQuery(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; set; }
	}
}
