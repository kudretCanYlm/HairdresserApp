using Database.Infrastructure;
using Database.Repository;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using User.Domain.Interfaces;
using User.Domain.Models;
using User.Infrastructure.Context;

namespace User.Infrastructure.Repository.User
{
	public class UserRepository : RepositoryBase<UserModel, UserContext>, IUserRepository
	{
		public UserRepository(IDatabaseFactory<UserContext> context):base(context) 
		{

		}

		public async Task<UserModel> GetByEmail(string email)
		{
			return await Get(x => x.Email == email);
		}

		public async Task<bool> IsEmailAlreadyUsing(string email)
		{
			return await GetManyQuery(x => x.Email == email).AnyAsync();
		}

		public async Task<bool> IsUserExist(Guid id)
		{
			return await GetManyQuery(x => x.Id == id).AnyAsync();

		}


	}
}
