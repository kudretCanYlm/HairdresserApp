using Database.Entity;
using System.Linq.Expressions;

namespace Database.Specifications
{
	public class NotSpecification<T> : BaseSpecification<T> where T : BaseEntity
	{
		private readonly ISpecification<T> _specification;
		private readonly Expression _not;
		private ParameterExpression _parameter;

		public NotSpecification(ISpecification<T> specification)
		{
			_specification = specification;
			_parameter = Expression.Parameter(typeof(T), specification.Criteria.Parameters.First().Name);
			_not = Expression.Not(Expression.Invoke(specification.Criteria, _parameter));
		}

		public override Expression<Func<T, bool>> Criteria =>
			Expression.Lambda<Func<T, bool>>(_not, _parameter);
	}
}
