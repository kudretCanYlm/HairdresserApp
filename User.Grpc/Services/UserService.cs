using AutoMapper;
using Grpc.Core;
using Grpc.User.Protos;
using User.Application.Interfaces.Address;

namespace User.GRPC.Services
{
	public class UserService:UserProtoService.UserProtoServiceBase
	{
		private readonly IAddressAppService _addressAppService;
		private readonly IMapper _mapper;

		public UserService(IAddressAppService addressAppService, IMapper mapper)
		{
			_addressAppService = addressAppService;
			_mapper = mapper;
		}

		public override async Task<UserAddress> GetSelectedUserAddressById(GetSelectedUserAddressByIdRequest request, ServerCallContext context)
		{
			//will change 
			//add GetUserSelectedAdressById method
			var userId = Guid.Parse(request.UserId);

			var addresses = await _addressAppService.GetAllByUserId(userId);
			var address=addresses.FirstOrDefault();

			var userAddress=_mapper.Map<UserAddress>(address);

			return userAddress;
		}

	}
}
