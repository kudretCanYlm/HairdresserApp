using AutoMapper;
using HairdresserService.Application.Dto;
using HairdresserService.Domain.Commands.HairdresserService;

namespace HairdresserService.Application.Mapper
{
	public class DtoToCommand:Profile
	{
		public DtoToCommand()
		{
			CreateMap<ActivateHairdresserServiceDto, ActivateHairdresserServiceCommand>();
			CreateMap<ActivateHairdresserServiceDto, DeactivateHairdresserServiceCommand>();
			CreateMap<CreateHairdresserServiceDto, CreateHairdresserServiceCommand>();
			CreateMap<DeleteHairdresserServiceDto, DeleteHairdresserServiceCommand>();
			CreateMap<UpdateHairdresserServiceDto, UpdateHairdresserServiceCommand>();			
		}
	}
}
