using Database.Repository;
using HairdresserService.Domain.Models;

namespace HairdresserService.Domain.Interfaces
{
	public interface IHairdresserServiceRepository:IBaseRepository<HairdresserServiceModel>
	{
		Task<HairdresserServiceModel> GetByIdAndHairdresserId(Guid id,Guid hairdresserId);
	}
}
