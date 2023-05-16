using AutoMapper;
using Hairdresser.Domain.Models;
using Hairdresser.Domain.Commands.Hairdresser;
using Events.Hairdresser;

namespace Hairdresser.Domain.Mapper
{
	public class DomainToCommandProfile:Profile
	{
		public DomainToCommandProfile()
		{
			//Hairdresser
			CreateMap<CreateHairdresserCommand, HairdresserModel>().ReverseMap();
			CreateMap<DeleteHairdresserCommand, HairdresserModel>().ReverseMap();
			CreateMap<UpdateHairdresserCommand, HairdresserModel>().ReverseMap();
			CreateMap<HairdresserModel, HairdresserCreatedEvent>().ReverseMap();
			CreateMap<HairdresserModel, HairdresserDeletedEvent>().ReverseMap();
			CreateMap<HairdresserModel, HairdresserUpdatedEvent>().ReverseMap();
		}
	}
}
