using MediatR;
using NetDevPack.Messaging;
using User.Domain.Models;

namespace User.Domain.Queries.Address
{
	public class GetUserAddressesByUserId: IRequest<IEnumerable<AddressModel>>
	{
		public GetUserAddressesByUserId(Guid userId)
		{
			UserId = userId;
		}

		public Guid UserId { get; set; }
	}
}
