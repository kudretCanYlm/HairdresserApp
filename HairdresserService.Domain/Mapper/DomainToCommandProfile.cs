using AutoMapper;
using Events.HairdresserService;
using HairdresserService.Domain.Commands.HairdresserService;
using HairdresserService.Domain.Models;

namespace HairdresserService.Domain.Mapper
{
	public class DomainToCommandProfile : Profile
	{
		public DomainToCommandProfile()
		{

			CreateMap<ActivateHairdresserServiceCommand, HairdresserServiceModel>().ReverseMap();
			CreateMap<CreateHairdresserServiceCommand, HairdresserServiceModel>().ReverseMap();
			CreateMap<DeactivateHairdresserServiceCommand, HairdresserServiceModel>().ReverseMap();
			CreateMap<DeleteHairdresserServiceCommand, HairdresserServiceModel>().ReverseMap();
			CreateMap<UpdateHairdresserServiceCommand, HairdresserServiceModel>().ReverseMap();

			CreateMap<HairdresserServiceModel,HairdresserServiceActivatedEvent>().ReverseMap();
			CreateMap<HairdresserServiceModel,HairdresserServiceCreatedEvent>().ReverseMap();
			CreateMap<HairdresserServiceModel,HairdresserServiceDeactivatedEvent>().ReverseMap();
			CreateMap<HairdresserServiceModel,HairdresserServiceDeletedEvent>().ReverseMap();
			CreateMap<HairdresserServiceModel,HairdresserServiceUpdatedEvent>().ReverseMap();
		}
	}
}
