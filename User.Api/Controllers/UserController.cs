using Events.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Mediator;
using Newtonsoft.Json;
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

		public UserController(IUserAppService userAppService, IMediatorHandler mediator)
		{
			_userAppService = userAppService;
			_mediator = mediator;
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
	}
}
