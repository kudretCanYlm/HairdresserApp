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
	}
}
