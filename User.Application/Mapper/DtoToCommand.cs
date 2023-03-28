using AutoMapper;
using User.Application.Dto.Address;
using User.Application.Dto.User;
using User.Domain.Commands.Address;
using User.Domain.Commands.User;

namespace User.Application.Mapper
{
	public class DtoToCommand:Profile
	{
		public DtoToCommand()
		{
			CreateMap<CreateUserDto, CreateUserCommand>().ReverseMap();
			CreateMap<UpdateUserDto, UpdateUserCommand>().ReverseMap();
			CreateMap<CreateAddressDto,CreateUserAddressCommand>().ReverseMap();
			CreateMap<UpdateAddressDto,UpdateUserAddressCommand>().ReverseMap();
		}
	}
}
