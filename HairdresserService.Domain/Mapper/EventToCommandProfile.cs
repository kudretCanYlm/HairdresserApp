using AutoMapper;
using Events.HairdresserService;
using HairdresserService.Domain.Commands.HairdresserService;

namespace HairdresserService.Domain.Mapper
{
	public class EventToCommandProfile:Profile
	{
		public EventToCommandProfile()
		{
			CreateMap<HairdresserServiceActivatedEvent, ActivateHairdresserServiceCommand>().ReverseMap();
			CreateMap<HairdresserServiceCreatedEvent, CreateHairdresserServiceCommand>().ReverseMap();
			CreateMap<HairdresserServiceDeactivatedEvent, DeactivateHairdresserServiceCommand>().ReverseMap();
			CreateMap<HairdresserServiceDeletedEvent, DeleteHairdresserServiceCommand>().ReverseMap();
			CreateMap<HairdresserServiceUpdatedEvent, UpdateHairdresserServiceCommand>().ReverseMap();
		}
	}
}
