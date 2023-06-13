using Database.Entity;
using System.Linq.Expressions;

namespace Database.Specifications
{
	public class AndSpecification<T> : BaseSpecification<T> where T : BaseEntity
	{
		private readonly ISpecification<T> _leftSpecification;
		private readonly ISpecification<T> _rightSpecification;
		private readonly Expression _and;
		private ParameterExpression _parameter;

		public AndSpecification(ISpecification<T> leftSpecification, ISpecification<T> rightSpecification)
		{
			_leftSpecification = leftSpecification;
			_rightSpecification = rightSpecification;
			_parameter = Expression.Parameter(typeof(T), leftSpecification.Criteria.Parameters.First().Name);
			_and = Expression.AndAlso(Expression.Invoke(_leftSpecification.Criteria,_parameter), Expression.Invoke(_rightSpecification.Criteria, _parameter));
		}

		public override Expression<Func<T, bool>> Criteria =>
			Expression.Lambda<Func<T, bool>>(_and,_parameter);
	}
}
