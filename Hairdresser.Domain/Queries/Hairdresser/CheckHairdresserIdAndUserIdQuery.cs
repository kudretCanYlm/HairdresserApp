using MediatR;

namespace Hairdresser.Domain.Queries.Hairdresser
{
	public class CheckHairdresserIdAndUserIdQuery:IRequest<bool>
	{
		public CheckHairdresserIdAndUserIdQuery(Guid id, Guid userId)
		{
			Id = id;
			UserId = userId;
		}

		public Guid Id { get; set; }
		public Guid UserId { get; set; }
	}
}
