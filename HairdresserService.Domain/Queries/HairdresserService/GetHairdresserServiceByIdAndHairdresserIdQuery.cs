using HairdresserService.Domain.Models;
using MediatR;

namespace HairdresserService.Domain.Queries.HairdresserService
{
	public class GetHairdresserServiceByIdAndHairdresserIdQuery:IRequest<HairdresserServiceModel>
	{
		public GetHairdresserServiceByIdAndHairdresserIdQuery(Guid id, Guid hairdresserId)
		{
			Id = id;
			HairdresserId = hairdresserId;
		}

		public Guid Id { get; set; }
		public Guid HairdresserId { get; set; }
	}
}
