using Media.Domain.Interfaces;
using Media.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Media.Domain.Queries.Media
{
	public class MediaQueryHandler : IRequestHandler<GetAllMediasQuery, IEnumerable<MediaModel>>,
									IRequestHandler<GetMediaByImageOwnerIdAndTypeQuery,MediaModel>,
									IRequestHandler<GetMediaListByImageOwnerIdAndTypeQuery,IEnumerable<MediaModel>>,
									IRequestHandler<GetMediaCountByImageOwnerIdAndTypeQuery,int>,
									IRequestHandler<IsMediaAvailableQuery,bool>
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

		public async Task<int> Handle(GetMediaCountByImageOwnerIdAndTypeQuery request, CancellationToken cancellationToken)
		{
			var count = await mediaRepository.GetManyQuery(x => x.ImageOwnerId == request.ImageOwnerId && x.CustomType == request.CustomType).CountAsync();

			return count;
		}

		public async Task<bool> Handle(IsMediaAvailableQuery request, CancellationToken cancellationToken)
		{
			var isAvaliable=await mediaRepository.GetManyQuery(x=>x.Id==request.Id && x.ImageOwnerId==request.OwnerId).AnyAsync();

			return isAvaliable;
		}
	}
}
