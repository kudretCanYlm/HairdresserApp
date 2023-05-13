using AutoMapper;
using Grpc.Media.Protos;
using Media.Application.Dto;

namespace Media.GRPC.Mapper
{
	public class DtoToProtoModel:Profile
	{
		public DtoToProtoModel()
		{
			CreateMap<MediaDto, MediaModel>()
				.ForMember(x => x.Id, x => x.MapFrom(x => x.Id.ToString()))
				.ForMember(x => x.ImageOwnerId, x => x.MapFrom(x => x.ImageOwnerId.ToString()));
		}
	}
}
