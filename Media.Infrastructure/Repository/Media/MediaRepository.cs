using Database.Infrastructure;
using Database.Repository;
using Media.Domain.Interfaces;
using Media.Domain.Models;
using Media.Infrastructure.Context;

namespace Media.Infrastructure.Repository.Media
{
	public class MediaRepository:RepositoryBase<MediaModel,MediaContext>,IMediaRepository
	{
		public MediaRepository(IDatabaseFactory<MediaContext> context) : base(context)
		{

		}

		public async Task<IReadOnlyList<MediaModel>> GetAllMediasByImageOwnerId(Guid imageOwnerId)
		{
			var medias= await GetMany(x => x.ImageOwnerId == imageOwnerId);
			return medias;
		}

		public async Task<MediaModel> GetMediaByImageOwnerIdAndCustomType(Guid imageOwnerId, string imageType)
		{
			var media = await Get(x => x.ImageOwnerId == imageOwnerId && x.CustomType== imageType.ToLower());
			return media;
		}
	}
}
