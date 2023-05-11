using Events.User;
using Grpc.Auth;
using Grpc.Auth.ClientServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Mediator;
using Newtonsoft.Json;
using System.Security.Claims;
using User.Application.Dto.User;
using User.Application.Interfaces.User;

namespace User.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IUserAppService _userAppService;
		private readonly IMediatorHandler _mediator;
		private readonly AuthGrpcService authGrpcService;
		private readonly ILogger<UserController> _logger;

		public UserController(IUserAppService userAppService, IMediatorHandler mediator, AuthGrpcService authGrpcService, ILogger<UserController> logger)
		{
			_userAppService = userAppService;
			_mediator = mediator;
			this.authGrpcService = authGrpcService;
			_logger = logger;
		}

		[HttpPost, AllowAnonymous]
		public async Task<IActionResult> Index([FromBody] CreateUserDto createUserDto)
		{

			var result = await _userAppService.CreateAsync(createUserDto);


			var history = await _userAppService.GetAllHistoryAsync(Guid.Parse("47c02319-a2c4-40c8-9e75-08db516ff33e"));

			return Created("Test", history);
		}
		[HttpGet, Route("tokentest/{token}"), AllowAnonymous]
		public async Task<IActionResult> Gettomen(string token)
		{
			var user = await authGrpcService.CheckTokenRefreshAndUserId(token);

			return Ok(user);
		}

		[HttpDelete, Route("delete"), AllowAnonymous]
		public async Task<IActionResult> Del(Guid id)
		{
			var result=await _userAppService.RemoveAsync(id);

			return Ok(result);
		}

		[HttpGet, Route("melih")]
		public async Task<IActionResult> GetMelih()
		{
			var user = HttpContext.GetCurrentUserId();
			return Ok(user);
		}

		[HttpGet, Route("melih2")]
		public async Task<IActionResult> GetMelih2()
		{
			_logger.LogError("hahta");
			return Ok("asd");
		}

		[HttpGet, Route("test13"), AllowAnonymous]
		public async Task<IActionResult> GEtTest()
		{
			var user = await _userAppService.GetAllAsync();

			return Ok(user);
		}
	}
}
