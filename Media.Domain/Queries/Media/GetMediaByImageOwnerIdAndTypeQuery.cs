using Media.Domain.Models;
using MediatR;

namespace Media.Domain.Queries.Media
{
	public class GetMediaByImageOwnerIdAndTypeQuery:IRequest<MediaModel>
	{
		public GetMediaByImageOwnerIdAndTypeQuery()
		{

		}
		public GetMediaByImageOwnerIdAndTypeQuery(string customType, Guid imageOwnerId)
		{
			CustomType = customType;
			ImageOwnerId = imageOwnerId;
		}

		public string CustomType { get; set; }
		public Guid ImageOwnerId { get; set; }
	}
}
