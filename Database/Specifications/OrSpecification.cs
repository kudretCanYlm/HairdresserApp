using Database.Entity;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace Database.Specifications
{
	public class OrSpecification<T> : BaseSpecification<T> where T : BaseEntity
	{
		private readonly ISpecification<T> _leftSpecification;
		private readonly ISpecification<T> _rightSpecification;
		private readonly Expression _or;
		private ParameterExpression _parameter;

		public OrSpecification(ISpecification<T> leftSpecification, ISpecification<T> rightSpecification)
		{
			_leftSpecification = leftSpecification;
			_rightSpecification = rightSpecification;
			_parameter = Expression.Parameter(typeof(T), leftSpecification.Criteria.Parameters.First().Name);
			_or = Expression.OrElse(Expression.Invoke(_leftSpecification.Criteria, _parameter), Expression.Invoke(_rightSpecification.Criteria, _parameter));
		}

		public override Expression<Func<T, bool>> Criteria =>
			Expression.Lambda<Func<T, bool>>(_or, _parameter);
			
	}
}
