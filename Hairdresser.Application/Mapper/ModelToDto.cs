using AutoMapper;
using Hairdresser.Application.Dto;
using Hairdresser.Domain.Models;

namespace Hairdresser.Application.Mapper
{
	public class ModelToDto:Profile
	{
		public ModelToDto()
		{
			CreateMap<HairdresserModel, HairdresserDto>();
		}
	}
}
