using Database.Entity;
using System.Linq.Expressions;

namespace Database.PaggingAndFilter
{
	public static class PagingExtensions
	{
		public static IQueryable<T> MyOrderBy<T>(this IQueryable<T> query, List<(SortingOption, Expression<Func<T, object>>)> orderByList) where T : BaseEntity
		{
			if (orderByList == null)
				return query;

			orderByList = orderByList.OrderBy(ob => ob.Item1.Priority).ToList();

			IOrderedQueryable<T> orderedQuery = null;

			foreach (var orderBy in orderByList)
			{
				if (orderedQuery == null)
					orderedQuery = orderBy.Item1.Direction == SortingDirection.ASC ? query.OrderBy(orderBy.Item2) : query.OrderByDescending(orderBy.Item2);
				else
					orderedQuery = orderBy.Item1.Direction == SortingDirection.ASC ? orderedQuery.ThenBy(orderBy.Item2) : orderedQuery.ThenByDescending(orderBy.Item2);
			}

			return orderedQuery;
		}

		public static IQueryable<T> MyWhere<T>(this IQueryable<T> query, List<(FilteringOption, Expression<Func<T, bool>>)> filterList = null) where T : BaseEntity
		{
			if (filterList == null)
				return query;

			foreach (var item in filterList)
			{
				query=query.Where(item.Item2);
			}

			return query;
		}
	}
}
