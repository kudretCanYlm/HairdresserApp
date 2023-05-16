using Database.Infrastructure;
using Database.Repository;
using Hairdresser.Domain.Interfaces;
using Hairdresser.Domain.Models;
using Hairdresser.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Hairdresser.Infrastructure.Repository.Hairdresser
{
	public class HairdresserRepository : RepositoryBase<HairdresserModel, HairdresserContext>, IHairdresserRepository
	{
		public HairdresserRepository(IDatabaseFactory<HairdresserContext> context):base(context)
		{

		}
		public async Task<HairdresserModel> GetByIdAndOwnerIdAsync(Guid id, Guid ownerId)
		{
			return await GetManyQuery(x=>x.Id==id && x.OwnerId==ownerId).FirstOrDefaultAsync();
		}
	}
}
