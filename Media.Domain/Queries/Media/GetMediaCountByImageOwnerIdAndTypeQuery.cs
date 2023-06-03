using MediatR;

namespace Media.Domain.Queries.Media
{
	public class GetMediaCountByImageOwnerIdAndTypeQuery:IRequest<int>
	{
		public GetMediaCountByImageOwnerIdAndTypeQuery()
		{

		}
		public GetMediaCountByImageOwnerIdAndTypeQuery(string customType, Guid imageOwnerId)
		{
			CustomType = customType;
			ImageOwnerId = imageOwnerId;
		}

		public string CustomType { get; set; }
		public Guid ImageOwnerId { get; set; }
	}
}
