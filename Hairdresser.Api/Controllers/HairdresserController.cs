using Grpc.Auth;
using Hairdresser.Application.Dto;
using Hairdresser.Application.Interfaces.Hairdresser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Hairdresser.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class HairdresserController : ControllerBase
	{
		private readonly IHairdresserAppService _hairdresserAppService;

		public HairdresserController(IHairdresserAppService hairdresserAppService)
		{
			_hairdresserAppService = hairdresserAppService;
		}

		[HttpPost,Route("AddHairdresser")]
		public async Task<IActionResult> AddHairdresser([FromBody]CreateHairdresserDto? createHairdresserDto)
		{
			createHairdresserDto.OwnerId = (Guid)GrpcAuthExtension.GetCurrentUserId(HttpContext);

			var result = await _hairdresserAppService.CreateAsync(createHairdresserDto);
			return Ok(result);
		}

		[HttpGet,Route("GetAllHairdressers")]
		public async Task<IActionResult> GetAllHairdressers()
		{
			var result = await _hairdresserAppService.GetAllHairdressers();

			return Ok(result);
		}

	}
}
