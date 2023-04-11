using Auth.Application.Dto.Auth;
using Auth.Domain.Models;
using AutoMapper;

namespace Auth.Application.Mapper
{
	public class ModelToDto:Profile
	{
		public ModelToDto()
		{
			CreateMap<AuthSessionModel, UserAuthSessionDto>().ReverseMap();
		}
	}
}
