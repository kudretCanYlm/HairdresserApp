using AutoMapper;
using Events.User;
using Events.User.Address;
using User.Domain.Commands.Address;
using User.Domain.Commands.User;
using User.Domain.Models;

namespace User.Domain.Mapper
{
	public class DomainToCommandProfile : Profile
	{
		public DomainToCommandProfile()
		{
			//User
			CreateMap<CreateUserCommand, UserModel>().ReverseMap();
			CreateMap<DeleteUserCommand, UserModel>().ReverseMap();
			CreateMap<UpdateUserCommand, UserModel>().ReverseMap();
			CreateMap<UserModel, UserCreatedEvent>().ReverseMap();
			CreateMap<UserModel, UserDeletedEvent>().ReverseMap();
			CreateMap<UserModel, UserUpdatedEvent>().ReverseMap();

			//Address
			CreateMap<CreateUserAddressCommand, AddressModel>().ReverseMap();
			CreateMap<DeleteUserAddressCommand, AddressModel>().ReverseMap();
			CreateMap<UpdateUserAddressCommand, AddressModel>().ReverseMap();
			CreateMap<AddressModel, UserAddressCreatedEvent>().ReverseMap();
			CreateMap<AddressModel, UserAddressDeletedEvent>().ReverseMap();
			CreateMap<AddressModel, UserAddressUpdatedEvent>().ReverseMap();
		}
	}
}
