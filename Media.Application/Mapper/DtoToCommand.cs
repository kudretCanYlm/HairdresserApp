using AutoMapper;
using Media.Application.Dto;
using Media.Domain.Commands.Media;
using Media.Domain.Models;

namespace Media.Application.Mapper
{
	public class DtoToCommand:Profile
	{
		public DtoToCommand()
		{
			CreateMap<CreateMediaDto,CreateMediaCommand>()
				.ForMember(x=>x.FileExtension,x=>x.MapFrom(x=>MediaModel.ToFileExtension(x.Base64Media)))
				.ForMember(x => x.MediaData, act => act.MapFrom(x => MediaModel.ToByteArray(x.Base64Media)));
			
			CreateMap<UpdateMediaDto,UpdateMediaCommand>()
				.ForMember(x => x.FileExtension, x => x.MapFrom(x => MediaModel.ToFileExtension(x.Base64Media)))
				.ForMember(x => x.MediaData, act => act.MapFrom(x => MediaModel.ToByteArray(x.Base64Media)));
		}
	}
}
