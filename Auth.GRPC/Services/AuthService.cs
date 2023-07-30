using Auth.Application.Interfaces.Auth;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Auth.Protos;
using Grpc.Core;

namespace Auth.GRPC.Services
{
	public class AuthService : AuthProtoService.AuthProtoServiceBase
	{
		IAuthAppService _authAppService;
		IMapper _mapper;

		public AuthService(IAuthAppService authAppService, IMapper mapper)
		{
			_authAppService = authAppService;
			_mapper = mapper;
		}

		public override async Task<UserModel> GetUser(GetUserRequest request, ServerCallContext context)
		{
			var userAuthSession = await _authAppService.CheckTokenAndAddExpiring(request.UserToken);
			if (userAuthSession == null)
			{
				var user = new UserModel()
				{
					UserId = Guid.Empty.ToString()
				};

				return user;
			}

			var userModel = new UserModel() { UserId = userAuthSession.TokenOwnerId.ToString() };

			return userModel;

		}

		public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
		{
			var result = await _authAppService.CreateToken(Guid.Parse(request.UserId));

			return new CreateUserResponse
			{
				Token = result.Token
			};
		}

		public override async Task<DeleteTokenResponse> DeleteToken(DeleteTokenRequest request, ServerCallContext context)
		{
			var result=await _authAppService.DeleteToken(request.Token);

			return new DeleteTokenResponse
			{
				IsDelete = result.IsValid
			};
		}

		public override async Task<DeleteTokenByIdResponse> DeleteTokenById(DeleteTokenByIdRequest request, ServerCallContext context)
		{
			var result = await _authAppService.DeleteTokenByIdAndUserId(Guid.Parse(request.TokenId), Guid.Parse(request.UserId));

			return new DeleteTokenByIdResponse 
			{
				IsDelete = result.IsValid 
			};

		}

		public override async Task<GetTokensForReviewListResponse> GetTokensForReview(GetTokensForReviewRequest request, ServerCallContext context)
		{
			var result = await _authAppService.GetAllTokens(Guid.Parse(request.UserId));

			var response = _mapper.Map<IEnumerable<GetTokensForReviewResponse>>(result);

			var resposeList = new GetTokensForReviewListResponse();

			resposeList.TokenList.AddRange(response);

			return resposeList;
			
		}
	}
}
