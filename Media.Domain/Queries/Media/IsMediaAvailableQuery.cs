using MediatR;

namespace Media.Domain.Queries.Media
{
	public class IsMediaAvailableQuery:IRequest<bool>
	{
		public IsMediaAvailableQuery(Guid id, Guid ownerId)
		{
			Id = id;
			OwnerId = ownerId;
		}

		public Guid Id { get; set; }
		public Guid OwnerId { get; set; }	
	}
}
