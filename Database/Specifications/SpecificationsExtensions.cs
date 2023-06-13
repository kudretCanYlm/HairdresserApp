using Database.Entity;

namespace Database.Specifications
{
	public static class SpecificationsExtensions
	{
		public static BaseSpecification<T> And<T>(this BaseSpecification<T> left, BaseSpecification<T> right) where T : BaseEntity
		{
			return new AndSpecification<T>(left, right);
		}

		public static BaseSpecification<T> Or<T>(this BaseSpecification<T> left, BaseSpecification<T> right) where T : BaseEntity
		{
			return new OrSpecification<T>(left, right);
		}

		public static BaseSpecification<T> Not<T>(this BaseSpecification<T> spec) where T : BaseEntity
		{
			return new NotSpecification<T>(spec);
		}
	}
}
