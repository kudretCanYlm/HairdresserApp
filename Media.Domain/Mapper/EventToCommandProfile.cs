using AutoMapper;
using Events.Media;
using Media.Domain.Commands.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Domain.Mapper
{
	public class EventToCommandProfile:Profile
	{
		public EventToCommandProfile()
		{
			CreateMap<MediaCreatedEvent,CreateMediaCommand>().ReverseMap();
			CreateMap<MediaDeletedEvent,DeleteMediaCommand>().ReverseMap();
			CreateMap<MediaUpdatedEvent,UpdateMediaCommand>().ReverseMap();
		}
	}
}
