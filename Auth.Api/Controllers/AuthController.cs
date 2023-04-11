using Microsoft.AspNetCore.Mvc;
using Auth.Application.Interfaces.Auth;
using Auth.Application.Dto.Auth;

namespace Auth.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthAppService _authAppService;

		public AuthController(IAuthAppService authAppService)
		{
			_authAppService = authAppService;
		}

		[HttpPost,Route("CreateSession")]
		public async Task<ActionResult<string>> CreateSession()
		{
			var result = await _authAppService.CreateToken(Guid.NewGuid());

			return Ok(result.TokenOwnerId);
		}

		[HttpGet,Route("RefleshToken/{token}")]
		public async Task<ActionResult<UserAuthSessionDto>> RefleshToken(string token)
		{
			var result = await _authAppService.CheckTokenAndAddExpiring(token);

			return Ok(result);
		}

		[HttpGet,Route("GetTokens/{tokenOwnerId}")]
		public async Task<ActionResult<UserAuthSessionDto>> GetTokens(Guid tokenOwnerId)
		{
			var result = await _authAppService.GetAllTokens(tokenOwnerId);

			return Ok(result);
		}


	}
}
