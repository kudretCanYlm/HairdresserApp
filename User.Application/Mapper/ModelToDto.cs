using AutoMapper;
using User.Application.Dto.Address;
using User.Application.Dto.User;
using User.Domain.Models;

namespace User.Application.Mapper
{
	public class ModelToDto:Profile
	{
		public ModelToDto()
		{
			CreateMap<UserModel, UserDto>();
			CreateMap<AddressModel, AddressDto>();
		}
	}
}
