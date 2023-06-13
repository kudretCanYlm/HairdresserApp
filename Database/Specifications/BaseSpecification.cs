using Database.Entity;
using System.Linq.Expressions;

namespace Database.Specifications
{
	public abstract class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
	{
		public abstract Expression<Func<T, bool>> Criteria { get; }
	}
}
