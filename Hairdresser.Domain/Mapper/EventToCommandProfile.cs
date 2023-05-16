using AutoMapper;
using Events.Hairdresser;
using Hairdresser.Domain.Commands.Hairdresser;

namespace Hairdresser.Domain.Mapper
{
	public class EventToCommandProfile:Profile
	{
		public EventToCommandProfile()
		{
			CreateMap<HairdresserCreatedEvent,CreateHairdresserCommand>().ReverseMap();
			CreateMap<HairdresserDeletedEvent,DeleteHairdresserCommand>().ReverseMap();
			CreateMap<HairdresserUpdatedEvent,UpdateHairdresserCommand>().ReverseMap();
		}
	}
}
