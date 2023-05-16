using Database.Repository;
using Hairdresser.Domain.Models;

namespace Hairdresser.Domain.Interfaces
{
	public interface IHairdresserRepository:IBaseRepository<HairdresserModel>
	{
		Task<HairdresserModel> GetByIdAndOwnerIdAsync(Guid id, Guid ownerId);
	}
}
