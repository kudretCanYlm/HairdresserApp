using Grpc.Auth.Protos;

namespace Grpc.Auth.ClientServices
{
	public class AuthGrpcService
	{
		private readonly AuthProtoService.AuthProtoServiceClient _authProtoService;

		public AuthGrpcService(AuthProtoService.AuthProtoServiceClient authProtoService)
		{
			_authProtoService = authProtoService;
		}

		public async Task<UserModel?> CheckTokenRefreshAndUserId(string token)
		{
			var authRequest = new GetUserRequest() { UserToken = token };
			var user = await _authProtoService.GetUserAsync(authRequest);

			return user;
		}

		public async Task<CreateUserResponse?> CreateNewUserToken(Guid userId)
		{
			var request = new CreateUserRequest
			{
				UserId = userId.ToString(),
			};

			var response = await _authProtoService.CreateUserAsync(request);

			return response;
		}

		public async Task<DeleteTokenResponse> DeleteUserToken(string token)
		{
			var request = new DeleteTokenRequest
			{
				Token = token
			};

			var response=await _authProtoService.DeleteTokenAsync(request);

			return response;
		}
	}
}
