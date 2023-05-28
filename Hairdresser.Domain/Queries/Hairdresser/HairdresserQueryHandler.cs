using Database.PaggingAndFilter;
using Hairdresser.Domain.Interfaces;
using Hairdresser.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hairdresser.Domain.Queries.Hairdresser
{
	public class HairdresserQueryHandler : IRequestHandler<GetAllHairdressersQuery, IEnumerable<HairdresserModel>>,
										IRequestHandler<GetHairdresserByIdQuery, HairdresserModel>,
										IRequestHandler<GetAllHairdresserByFilterQuery, IPagedList<HairdresserModel>>,
										IRequestHandler<CheckHairdresserIdAndUserIdQuery,bool>
	{
		private readonly IHairdresserRepository _hairdresserRepository;

		public HairdresserQueryHandler(IHairdresserRepository hairdresserRepository)
		{
			_hairdresserRepository = hairdresserRepository;
		}

		public async Task<IEnumerable<HairdresserModel>> Handle(GetAllHairdressersQuery request, CancellationToken cancellationToken)
		{
			var hairdressers = await _hairdresserRepository.GetAll();

			return hairdressers;
		}

		public async Task<HairdresserModel> Handle(GetHairdresserByIdQuery request, CancellationToken cancellationToken)
		{
			var hairdresser = await _hairdresserRepository.GetById(request.Id);

			return hairdresser;
		}

		public async Task<IPagedList<HairdresserModel>> Handle(GetAllHairdresserByFilterQuery request, CancellationToken cancellationToken)
		{
			var orderByList = new List<(SortingOption, Expression<Func<HairdresserModel, object>>)>();
			var args = request.PageSearchArgs;
			var query = _hairdresserRepository.Table;

			if (args.SortingOptions != null)
			{
				foreach (var sortingOption in args.SortingOptions)
				{
					switch (sortingOption.Field)
					{
						case "name":
							orderByList.Add((sortingOption, x => x.Name));
							break;
						case "createdAt":
							orderByList.Add((sortingOption, x => x.CreatedAt.ToString()));
							break;
						default:
							continue;
					}
				}
			}

			var filterList = new List<(FilteringOption, Expression<Func<HairdresserModel, bool>>)>();

			if (args.FilteringOptions != null)
			{
				foreach (var filteringOption in args.FilteringOptions)
				{
					switch (filteringOption.Field)
					{
						case "location":
							filterList.Add((filteringOption, f => f.Address.ToLower().Contains(filteringOption.Value.ToString().ToLower())));
							break;
						case "search":
							filterList.Add((filteringOption, f => f.Name.ToLower().Contains(filteringOption.Value.ToString().ToLower())));
							break;
						default:
							continue;
					}
				}
			}

			var pagedList = new PagedList<HairdresserModel>();
			
			await pagedList.CreatePagedList(query, 
				new PagingArgs 
				{
					PageIndex = args.PageIndex, 
					PageSize = args.PageSize, 
					PagingStrategy = args.PagingStrategy 
				},
				orderByList, filterList);

			return pagedList;
		}

		public async Task<bool> Handle(CheckHairdresserIdAndUserIdQuery request, CancellationToken cancellationToken)
		{
			var result = await _hairdresserRepository.GetManyQuery(x => x.Id == request.Id && x.OwnerId == request.UserId).AnyAsync();

			return result;
		}
	}
}
