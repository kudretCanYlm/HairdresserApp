using Database.PaggingAndFilter;
using Hairdresser.Domain.Models;
using MediatR;

namespace Hairdresser.Domain.Queries.Hairdresser
{
	public class GetAllHairdresserByFilterQuery:IRequest<IPagedList<HairdresserModel>>
	{
		public GetAllHairdresserByFilterQuery(PageSearchArgs pageSearchArgs)
		{
			this.PageSearchArgs = pageSearchArgs;
		}

		public PageSearchArgs PageSearchArgs { get; set; }
	}
}
