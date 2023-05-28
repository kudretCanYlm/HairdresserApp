using HairdresserService.Domain.Models;
using MediatR;

namespace HairdresserService.Domain.Queries.HairdresserService
{
	public class GetHairdresserServiceByIdQuery:IRequest<HairdresserServiceModel>
	{
		public GetHairdresserServiceByIdQuery(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; set; }
	}
}
