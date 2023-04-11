using NetDevPack.Domain;
using System.Linq.Expressions;

namespace Database.Repository.Redis
{
	public interface IRedisBaseRepository<T>:IDisposable where T : IAggregateRoot
	{
		Task<string> InsertAsync(T model);
		Task UpdateAsync(T model);
		Task DeleteAsync(T model);
		Task DeleteWhere(Expression<Func<T, bool>> where);
		Task<IEnumerable<T>> GetAllByWhereAsync(Expression<Func<T,bool>> where);
		Task<T> GetSingle(Expression<Func<T, bool>> where);

		Task AddExpire(string aggregateAndId, TimeSpan expiringTimeSeconds);
	}
}
