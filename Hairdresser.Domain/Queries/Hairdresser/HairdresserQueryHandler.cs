using Hairdresser.Domain.Interfaces;
using Hairdresser.Domain.Models;
using MediatR;

namespace Hairdresser.Domain.Queries.Hairdresser
{
	public class HairdresserQueryHandler : IRequestHandler<GetAllHairdressersQuery, IEnumerable<HairdresserModel>>,
										IRequestHandler<GetHairdresserByIdQuery, HairdresserModel>
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
	}
}
