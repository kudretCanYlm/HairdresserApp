using Grpc.User.Protos;

namespace Grpc.User.ClientServices
{
	public class UserGrpcService
	{
		private readonly UserProtoService.UserProtoServiceClient _userProtoServiceClient;

		public UserGrpcService(UserProtoService.UserProtoServiceClient userProtoServiceClient)
		{
			_userProtoServiceClient = userProtoServiceClient;
		}

		public async Task<UserAddress> GetSelectedUserAddressById(Guid userId)
		{
			var request = new GetSelectedUserAddressByIdRequest
			{
				UserId = userId.ToString()
			};

			var userAddress = await _userProtoServiceClient.GetSelectedUserAddressByIdAsync(request);

			return userAddress;
		}
	}
}
