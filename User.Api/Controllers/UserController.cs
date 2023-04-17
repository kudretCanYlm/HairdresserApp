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

		[HttpGet]
		public async Task<IActionResult> Index()
		{

			var user = new CreateUserDto
			{
				Email = "Test@gmail.com",
				Name = "Melih",
				Surname = "Bey",
				Password = "123454",
				Phone = "5467821456"
			};
			var result = await _userAppService.CreateAsync(user);

			//var Event = new UserDeletedEvent(Guid.Parse("60169185-2ba3-4aff-8e26-08db2ed52c4d"));
			//await _mediator.PublishEvent(Event);
			var history =await _userAppService.GetAllHistoryAsync(Guid.Parse("64169185-2ba3-4aff-8e26-08db2ed52c4d"));

			return Created("Test",history);
		}
		[HttpGet,Route("tokentest/{token}"),AllowAnonymous]
		public async Task<IActionResult> Gettomen(string token)
		{
			var user = await authGrpcService.CheckTokenRefreshAndUserId(token);

			return Ok(user);
		}

		[HttpGet,Route("melih")]
		public async Task<IActionResult> GetMelih()
		{
			var user = HttpContext.GetCurrentUserId();
			return Ok(user);
		}

		[HttpGet,Route("melih2")]
		public async Task<IActionResult> GetMelih2()
		{
			_logger.LogError("hahta");
			return Ok("asd");
		}
	}
}
