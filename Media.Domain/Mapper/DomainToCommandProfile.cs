using AutoMapper;
using Events.Media;
using Media.Domain.Commands.Media;
using Media.Domain.Models;

namespace Media.Domain.Mapper
{
	public class DomainToCommandProfile : Profile
	{
		public DomainToCommandProfile()
		{
			//Media
			CreateMap<CreateMediaCommand, MediaModel>().ReverseMap();
			CreateMap<DeleteMediaCommand, MediaModel>().ReverseMap();
			CreateMap<UpdateMediaCommand, MediaModel>().ReverseMap();
			CreateMap<MediaModel, MediaCreatedEvent>().ReverseMap();
			CreateMap<MediaModel, MediaDeletedEvent>().ReverseMap();
			CreateMap<MediaModel, MediaUpdatedEvent>().ReverseMap();

		}
	}
}
