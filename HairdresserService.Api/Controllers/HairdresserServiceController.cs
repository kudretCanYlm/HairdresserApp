using HairdresserService.Application.Dto;
using HairdresserService.Application.Interfaces.HairdresserService;
using Microsoft.AspNetCore.Mvc;

namespace HairdresserService.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class HairdresserServiceController : ControllerBase
	{
		private readonly IHairdresserServiceAppService _hairdresserServiceAppService;

		public HairdresserServiceController(IHairdresserServiceAppService hairdresserServiceAppService)
		{
			_hairdresserServiceAppService = hairdresserServiceAppService;
		}

		[HttpPost,Route("AddHairdresserService")]
		public async Task<IActionResult> AddHairdresserService([FromBody] CreateHairdresserServiceDto createHairdresserServiceDto)
		{
			var result=await _hairdresserServiceAppService.CreateHairdresserService(createHairdresserServiceDto);

			if (result.IsValid)
				return Ok(result);
			else
				return BadRequest(result);
		}
	}
}
