using Database.Repository;
using User.Domain.Models;

namespace User.Domain.Interfaces
{
	public interface IAddressRepository: IBaseRepository<AddressModel>
	{
		Task<IReadOnlyList<AddressModel>> GetAllByUserId(Guid userId);
		
	}
}
