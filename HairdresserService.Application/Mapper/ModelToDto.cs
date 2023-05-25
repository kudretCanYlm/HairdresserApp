using AutoMapper;
using HairdresserService.Application.Dto;
using HairdresserService.Domain.Models;

namespace HairdresserService.Application.Mapper
{
	public class ModelToDto:Profile
	{
		public ModelToDto()
		{
			CreateMap<HairdresserServiceModel, HairdresserServiceDto>();
		}
	}
}
