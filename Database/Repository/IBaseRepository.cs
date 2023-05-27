using Database.Entity;
using NetDevPack.Data;
using NetDevPack.Domain;
using System.Linq.Expressions;

namespace Database.Repository
{
	public interface IBaseRepository<T>:IRepository<T> where T:BaseEntity,IAggregateRoot
	{
		IQueryable<T> Table { get;}
		void Add(T entity);
		void Update(T entity);
		void UpdateMany(IEnumerable<T> entities);
		void Delete(T entity);
		void Delete(Expression<Func<T, bool>> where);
		Task<T> GetById(Guid id);
		Task<T> GetById(string id);
		Task<T> Get(Expression<Func<T, bool>> where);
		Task<IReadOnlyList<T>> GetAll();
		Task<IReadOnlyList<T>> GetMany(Expression<Func<T, bool>> where);
		IQueryable<T> GetManyQuery(Expression<Func<T, bool>> where);
	}
}
