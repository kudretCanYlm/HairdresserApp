using AutoMapper;
using Media.Application.Dto;
using Media.Domain.Models;

namespace Media.Application.Mapper
{
	public class ModelToDto:Profile
	{
		public ModelToDto()
		{
			CreateMap<MediaModel, MediaDto>()
				.ForMember(x => x.Base64Media, x => x.MapFrom(x => MediaModel.ToBase64(x)));
		}
	}
}
