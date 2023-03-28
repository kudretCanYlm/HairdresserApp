using MediatR;
using NetDevPack.Messaging;
using User.Domain.Models;

namespace User.Domain.Queries.Address
{
	public class GetAllUserAddressesQuery:Command,IRequest<IEnumerable<AddressModel>>
	{
		public GetAllUserAddressesQuery()
		{

		}
	}
}
