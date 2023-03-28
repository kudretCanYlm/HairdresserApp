using Database.Infrastructure;
using Database.Repository;
using User.Domain.Interfaces;
using User.Domain.Models;
using User.Infrastructure.Context;

namespace User.Infrastructure.Repository.Address
{
	public class AddressRepository :RepositoryBase<AddressModel,UserContext>, IAddressRepository
	{
		public AddressRepository(IDatabaseFactory<UserContext> context) :base(context)
		{

		}

		public async Task<IReadOnlyList<AddressModel>> GetAllByUserId(Guid userId)
		{
			return await GetMany(x => x.UserId == userId);
		}
	}
}
