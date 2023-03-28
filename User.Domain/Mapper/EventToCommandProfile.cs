using AutoMapper;
using Events.User;
using Events.User.Address;
using User.Domain.Commands.Address;
using User.Domain.Commands.User;

namespace User.Domain.Mapper
{
	public class EventToCommandProfile:Profile
	{
		public EventToCommandProfile()
		{
			//User
			CreateMap<UserCreatedEvent, CreateUserCommand>().ReverseMap();
			CreateMap<UserDeletedEvent, DeleteUserCommand>().ReverseMap();
			CreateMap<UserUpdatedEvent, UpdateUserCommand>().ReverseMap();

			//Address
			CreateMap<UserAddressCreatedEvent, CreateUserAddressCommand>().ReverseMap();
			CreateMap<UserAddressDeletedEvent, DeleteUserAddressCommand>().ReverseMap();
			CreateMap<UserAddressUpdatedEvent, UpdateUserAddressCommand>().ReverseMap();
		}

	}
}
