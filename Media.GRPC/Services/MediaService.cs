using AutoMapper;
using Google.Protobuf.Collections;
using Grpc.Core;
using Grpc.Media.Protos;
using Media.Application.Interfaces.Media;

namespace Media.GRPC.Services
{
	public class MediaService:MediaProtoService.MediaProtoServiceBase
	{
		private readonly IMediaAppService _mediaAppService;
		private readonly IMapper _mapper;

		public MediaService(IMediaAppService mediaAppService, IMapper mapper)
		{
			_mediaAppService = mediaAppService;
			_mapper = mapper;
		}

		public override async Task<MediaModelList> GetAllMediasByOwnerId(GetAllMediasByOwnerIdRequest request, ServerCallContext context)
		{
			var medias = await _mediaAppService.GetAllByOwnerIdAsync(Guid.Parse(request.ImageOwnerId));

			var mediaModelList = new MediaModelList();

			mediaModelList.Medias.AddRange(_mapper.Map<IEnumerable<MediaModel>>(medias));

			return mediaModelList;
			
		}

		public override async Task<MediaModel> GetMediaByOwnerIdAndType(GetMediaByOwnerIdAndTypeRequest request, ServerCallContext context)
		{
			var media = await _mediaAppService.GetByOwnerIdAndImageType(Guid.Parse(request.ImageOwnerId), request.Type);

			return _mapper.Map<MediaModel>(media);
		}
	}
}
