using Media.Domain.Models;
using MediatR;

namespace Media.Domain.Queries.Media
{
	public class GetMediaListByImageOwnerIdAndTypeQuery:IRequest<IEnumerable<MediaModel>>
	{
		public GetMediaListByImageOwnerIdAndTypeQuery(string customType, Guid imageOwnerId)
		{
			CustomType = customType;
			ImageOwnerId = imageOwnerId;
		}

		public string CustomType { get; set; }
		public Guid ImageOwnerId { get; set; }
	}
}
