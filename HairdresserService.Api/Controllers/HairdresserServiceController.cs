using Grpc.Auth;
using Grpc.Hairdresser.ClientServices;
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
		private readonly HairdresserGrpcService _hairdresserGrpcService;

		public HairdresserServiceController(IHairdresserServiceAppService hairdresserServiceAppService, HairdresserGrpcService hairdresserGrpcService)
		{
			_hairdresserServiceAppService = hairdresserServiceAppService;
			_hairdresserGrpcService = hairdresserGrpcService;
		}

		[HttpPost, Route("AddHairdresserService")]
		public async Task<IActionResult> AddHairdresserService([FromBody] CreateHairdresserServiceDto createHairdresserServiceDto)
		{
			var userId = (Guid)GrpcAuthExtension.GetCurrentUserId(HttpContext);

			var status = await _hairdresserGrpcService.CheckHairdresserIdAndUserId(createHairdresserServiceDto.HairdresserId, userId);

			if (!status)
				return BadRequest("This user is not owner");

			var result = await _hairdresserServiceAppService.CreateHairdresserService(createHairdresserServiceDto);

			if (result.IsValid)
				return Ok(result);
			else
				return BadRequest(result);
		}

		[HttpGet, Route("GetHairdresserServices/{hairdresserId:guid}")]
		public async Task<IActionResult> GetHairdresserServices(Guid hairdresserId)
		{
			var result = await _hairdresserServiceAppService.GetAllHairdresserServicesByHairdresserId(hairdresserId);

			return Ok(result);
		}
		//resimlerin idsinide getir
		[HttpGet, Route("GetHairdresserService/{serviceId:guid}")]
		public async Task<IActionResult> GetHairdresserService(Guid serviceId)
		{
			var result = await _hairdresserServiceAppService.GetAllHairdresserServiceWithImagesById(serviceId);

			if (result == null)
				return Ok(new List<string>());

			return Ok(result);
		}
	}
}
