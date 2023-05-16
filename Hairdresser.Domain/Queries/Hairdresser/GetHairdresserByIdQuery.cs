using Hairdresser.Domain.Models;
using MediatR;

namespace Hairdresser.Domain.Queries.Hairdresser
{
	public class GetHairdresserByIdQuery:IRequest<HairdresserModel>
	{
		public GetHairdresserByIdQuery(Guid ıd)
		{
			Id = ıd;
		}

		public Guid Id { get; set; }
	}
}
