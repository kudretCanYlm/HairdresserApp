using Media.Domain.Interfaces;
using Media.Domain.Models;
using MediatR;

namespace Media.Domain.Queries.Media
{
	public class MediaQueryHandler : IRequestHandler<GetAllMediasQuery, IEnumerable<MediaModel>>,
									IRequestHandler<GetMediaByImageOwnerIdAndTypeQuery,MediaModel>,
									IRequestHandler<GetMediaListByImageOwnerIdAndTypeQuery,IEnumerable<MediaModel>>
		{					
		private readonly IMediaRepository mediaRepository;

		public MediaQueryHandler(IMediaRepository mediaRepository)
		{
			this.mediaRepository = mediaRepository;
		}

		public async Task<IEnumerable<MediaModel>> Handle(GetAllMediasQuery request, CancellationToken cancellationToken)
		{
			var medias=await mediaRepository.GetAllMediasByImageOwnerId(request.ImageOwnerId);

			return medias;
		}

		public async Task<MediaModel> Handle(GetMediaByImageOwnerIdAndTypeQuery request, CancellationToken cancellationToken)
		{
			var media = await mediaRepository.GetMediaByImageOwnerIdAndCustomType(request.ImageOwnerId, request.CustomType);
			
			return media;
		}

		public async Task<IEnumerable<MediaModel>> Handle(GetMediaListByImageOwnerIdAndTypeQuery request, CancellationToken cancellationToken)
		{
			var mediaList=await mediaRepository.GetMany(x=>x.ImageOwnerId==request.ImageOwnerId && x.CustomType==request.CustomType);

			return mediaList;
		}
	}
}
