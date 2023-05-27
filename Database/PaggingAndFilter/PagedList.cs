using Database.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Database.PaggingAndFilter
{
	public class PagedList<T> : IPagedList<T> where T : BaseEntity
	{
		public PagedList()
		{

		}

		public PagedList(int pageIndex, int pageSize, int totalCount, int totalPages, IEnumerable<T> items)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
			TotalCount = totalCount;
			TotalPages = totalPages;
			Items = items;
		}

		public async Task CreatePagedList(IQueryable<T> query, PagingArgs pagingArgs, List<(SortingOption, Expression<Func<T, object>>)> orderByList = null, List<(FilteringOption, Expression<Func<T, bool>>)> filterList = null) 
		{
			query = query.MyOrderBy(orderByList);
			query = query.MyWhere(filterList);

			PageIndex = pagingArgs.PageIndex < 1 ? 1 : pagingArgs.PageIndex;
			PageSize = pagingArgs.PageSize < 1 ? 1 : pagingArgs.PageSize;

			TotalCount = 0;

			var items =
				pagingArgs.PagingStrategy == PagingStrategy.NoCount
				?
				await query.Skip((PageIndex - 1) * PageSize).Take(PageSize + 1).ToListAsync()
				:
				(
					(TotalCount = await query.CountAsync()) > 0
						?
					await	query.Skip((PageIndex - 1) * PageSize)
							.Take(PageSize).ToListAsync()
							: new List<T>()
				);


			TotalCount = pagingArgs.PagingStrategy == PagingStrategy.WithCount
				? (PageIndex - 1) * PageSize + items.Count : TotalCount;

			TotalPages = TotalCount / PageSize;

			if (TotalPages % PageSize > 0)
				TotalPages++;

			if (pagingArgs.PagingStrategy == PagingStrategy.NoCount && items.Count == PageSize + 1)
				items.RemoveAt(PageSize);

			Items = items;
		}
		public int PageIndex { get; private set; }

		public int PageSize { get; private set; }

		public int TotalCount { get; private set; }

		public int TotalPages { get; private set; }

		public bool HasPreviousPage => PageIndex > 0;

		public bool HasNextPage => PageIndex + 1 < TotalCount;

		public IEnumerable<T> Items { get; private set; }


	}
}
