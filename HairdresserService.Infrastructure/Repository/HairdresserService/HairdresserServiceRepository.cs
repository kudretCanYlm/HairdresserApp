using Database.Infrastructure;
using Database.Repository;
using HairdresserService.Domain.Interfaces;
using HairdresserService.Domain.Models;
using HairdresserService.Infrastructure.Context;

namespace HairdresserService.Infrastructure.Repository.HairdresserService
{
	public class HairdresserServiceRepository : RepositoryBase<HairdresserServiceModel, HairdresserServiceContext>, IHairdresserServiceRepository
	{
		public HairdresserServiceRepository(IDatabaseFactory<HairdresserServiceContext> context):base(context)
		{

		}

		public async Task<HairdresserServiceModel> GetByIdAndHairdresserId(Guid id, Guid hairdresserId)
		{
			var result=await Get(x=>x.Id==id && x.HairdresserId==hairdresserId);
			return result;
		}
	}
}
