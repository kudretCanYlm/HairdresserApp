using HairdresserService.Domain.Models;
using MediatR;

namespace HairdresserService.Domain.Queries.HairdresserService
{
	public class GetAllHairdresserServicesByHairdresserIdQuery:IRequest<IEnumerable<HairdresserServiceModel>>
	{
		public GetAllHairdresserServicesByHairdresserIdQuery()
		{

		}

		public GetAllHairdresserServicesByHairdresserIdQuery(Guid hairdresserId)
		{
			HairdresserId = hairdresserId;
		}

		public Guid HairdresserId { get; set; }
	}
}
