﻿using HairdresserService.Domain.Interfaces;
using HairdresserService.Domain.Models;
using MediatR;

namespace HairdresserService.Domain.Queries.HairdresserService
{
	public class HairdresserServiceQueryHandler : IRequestHandler<GetAllHairdresserServicesByHairdresserIdQuery, IEnumerable<HairdresserServiceModel>>,
													IRequestHandler<GetHairdresserServiceByIdQuery, HairdresserServiceModel>,
													IRequestHandler<GetHairdresserServiceByIdAndHairdresserIdQuery, HairdresserServiceModel>
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

		public async Task<HairdresserServiceModel> Handle(GetHairdresserServiceByIdQuery request, CancellationToken cancellationToken)
		{
			var result = await _hairdresserServiceRepository.GetById(request.Id);

			return result;
		}

		public async Task<HairdresserServiceModel> Handle(GetHairdresserServiceByIdAndHairdresserIdQuery request, CancellationToken cancellationToken)
		{
			//add Specification for the IsItActive
			var result =await _hairdresserServiceRepository.Get(x=>x.Id== request.Id && x.HairdresserId==request.HairdresserId &&x.IsItActive==true);
			
			return result;
		}
	}
}
