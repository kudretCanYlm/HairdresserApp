using MediatR;
namespace User.Domain.Queries.Address
{
	public class CheckAdressByUserIdQuery:IRequest<bool>
	{
		public CheckAdressByUserIdQuery(Guid userId)
		{
			UserId = userId;
		}

		public Guid UserId { get; set; }
	}
}
