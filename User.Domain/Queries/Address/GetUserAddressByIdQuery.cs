using MediatR;
using NetDevPack.Messaging;
using User.Domain.Models;

namespace User.Domain.Queries.Address
{
	public class GetUserAddressByIdQuery: IRequest<AddressModel>
	{
		public GetUserAddressByIdQuery(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; set; }
	}
}
