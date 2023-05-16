using Hairdresser.Domain.Models;
using MediatR;

namespace Hairdresser.Domain.Queries.Hairdresser
{
	public class GetAllHairdressersQuery:IRequest<IEnumerable<HairdresserModel>>
	{

	}
}
