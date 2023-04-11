using Auth.Application.Interfaces.Auth;
using Grpc.Auth.Protos;
using Grpc.Core;

namespace Auth.GRPC.Services
{
	public class AuthService : AuthProtoService.AuthProtoServiceBase
	{
		IAuthAppService _authAppService;

		public AuthService(IAuthAppService authAppService)
		{
			_authAppService = authAppService;
		}

		public override async Task<UserModel> GetUser(GetUserRequest request, ServerCallContext context)
		{
			var userAuthSession = await _authAppService.CheckTokenAndAddExpiring(request.UserToken);
			if (userAuthSession == null)
				return null;

			var userModel = new UserModel() { UserId = userAuthSession.TokenOwnerId.ToString() };

			return userModel;

		}
	}
}
