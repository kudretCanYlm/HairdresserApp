using Database.Repository;
using NetDevPack.Data;
using User.Domain.Models;

namespace User.Domain.Interfaces
{
	public interface IUserRepository:IBaseRepository<UserModel>
	{
		Task<UserModel> GetByEmail(string email);
		Task<bool> IsEmailAlreadyUsing(string email);
		Task<bool> IsEmailAlreadyUsingWithOutMy(string email,Guid userId);
		Task<bool> IsUserExist(Guid id);

	}
}
