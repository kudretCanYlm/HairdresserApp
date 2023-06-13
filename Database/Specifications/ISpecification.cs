using Database.Entity;
using NetDevPack.Domain;
using System.Linq.Expressions;

namespace Database.Specifications
{
	public interface ISpecification<T> where T:BaseEntity
	{
		Expression<Func<T, bool>> Criteria { get; }
	}
}
