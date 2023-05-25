using HairdresserService.Domain.Interfaces;
using HairdresserService.Domain.Models;
using MediatR;

namespace HairdresserService.Domain.Queries.HairdresserService
{
	public class HairdresserServiceQueryHandler : IRequestHandler<GetAllHairdresserServicesByHairdresserIdQuery, IEnumerable<HairdresserServiceModel>>
	{
		private readonly IHairdresserServiceRepository _hairdresserServiceRepository;

		public HairdresserServiceQueryHandler(IHairdresserServiceRepository hairdresserServiceRepository)
		{
			_hairdresserServiceRepository = hairdresserServiceRepository;
		}

		public async Task<IEnumerable<HairdresserServiceModel>> Handle(GetAllHairdresserServicesByHairdresserIdQuery request, CancellationToken cancellationToken)
		{
			var result = await _hairdresserServiceRepository.GetMany(x => x.HairdresserId == request.HairdresserId);

			return result;
		}
	}
}
