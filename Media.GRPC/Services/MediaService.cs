﻿using AutoMapper;
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

		public override async Task<MediaModelList> GetAllMediasByOwnerId(GetAllMediasByOwnerIdAndTypeRequest request, ServerCallContext context)
		{
			var medias = await _mediaAppService.GetMediaListByImageOwnerIdAndType(Guid.Parse(request.ImageOwnerId),request.Type);

			var mediaModelList = new MediaModelList();


			if (!medias.Any())
			{
				mediaModelList.Medias.Add(new MediaModel
				{
					Id=Guid.Empty.ToString(),
					Base64Media="",
					ImageOwnerId=Guid.Empty.ToString(),
				});

				return mediaModelList;
			}

			mediaModelList.Medias.AddRange(_mapper.Map<IEnumerable<MediaModel>>(medias));

			return mediaModelList;
			
		}

		public override async Task<MediaModel> GetMediaByOwnerIdAndType(GetMediaByOwnerIdAndTypeRequest request, ServerCallContext context)
		{
			var media = await _mediaAppService.GetByOwnerIdAndImageType(Guid.Parse(request.ImageOwnerId), request.Type);

			if (media == null)
				return new MediaModel
				{
					Base64Media="",
					Id=Guid.Empty.ToString(),
					ImageOwnerId= Guid.Empty.ToString()
				};

			return _mapper.Map<MediaModel>(media);
		}

		public override async Task<MediaCountModel> GetImageCount(GetImageCountRequest request, ServerCallContext context)
		{
			var count = await _mediaAppService.GetMediaCountByImageOwnerIdAndType(Guid.Parse(request.ImageOwnerId), request.Type);
			
			var mediaCountModel=new MediaCountModel 
			{
				Count=count 
			};

			return mediaCountModel;
		}

		public override async Task<IsMediaAvailableModel> IsMediaAvailable(IsMediaAvailableRequest request, ServerCallContext context)
		{
			var isAvaliable = await _mediaAppService.IsMediaAvailable(Guid.Parse(request.Id),Guid.Parse(request.ImageOwnerId));

			var isMediaAvaliableModel = new IsMediaAvailableModel
			{
				IsAvaliable = isAvaliable
			};

			return isMediaAvaliableModel;

		}
	}
}
