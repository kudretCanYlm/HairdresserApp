using Grpc.Media.Protos;


namespace Grpc.Media.ClientServices
{
	public class MediaGrpcService
	{
		private readonly MediaProtoService.MediaProtoServiceClient _mediaProtoService;

		public MediaGrpcService(MediaProtoService.MediaProtoServiceClient mediaProtoService)
		{
			_mediaProtoService = mediaProtoService;
		}

		public async Task<IEnumerable<MediaModel>> GetAllMediasByOwnerIdAndTypeAsync(Guid ownerId, string type)
		{
			var request = new GetAllMediasByOwnerIdAndTypeRequest
			{
				ImageOwnerId= ownerId.ToString(),
				Type= type
			};

			var mediaList = await _mediaProtoService.GetAllMediasByOwnerIdAsync(request);

			return mediaList.Medias;
		}

		public async Task<MediaModel> GetMediaByOwnerIdAndTypeAsync(Guid ownerId,string type)
		{
			var request = new GetMediaByOwnerIdAndTypeRequest
			{
				ImageOwnerId = ownerId.ToString(),
				Type=type
			};

			var media=await _mediaProtoService.GetMediaByOwnerIdAndTypeAsync(request);

			return media;
		}
	}
}
