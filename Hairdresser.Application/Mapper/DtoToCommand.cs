using AutoMapper;
using Hairdresser.Application.Dto;
using Hairdresser.Domain.Commands.Hairdresser;

namespace Hairdresser.Application.Mapper
{
	public class DtoToCommand:Profile
	{
		public DtoToCommand()
		{
			CreateMap<CreateHairdresserDto, CreateHairdresserCommand>();
			CreateMap<UpdateHairdresserDto, UpdateHairdresserCommand>();
		}
	}
}
