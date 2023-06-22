using Database.PaggingAndFilter;
using Grpc.Auth;
using Grpc.User.ClientServices;
using Hairdresser.Application.Dto;
using Hairdresser.Application.Interfaces.Hairdresser;
using Microsoft.AspNetCore.Mvc;

namespace Hairdresser.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class HairdresserController : ControllerBase
	{
		private readonly IHairdresserAppService _hairdresserAppService;
		private readonly UserGrpcService _userGrpcService;

		public HairdresserController(IHairdresserAppService hairdresserAppService, UserGrpcService userGrpcService)
		{
			_hairdresserAppService = hairdresserAppService;
			_userGrpcService = userGrpcService;
		}

		[HttpPost, Route("AddHairdresser")]
		public async Task<IActionResult> AddHairdresser([FromBody] CreateHairdresserDto? createHairdresserDto)
		{
			createHairdresserDto.OwnerId = (Guid)GrpcAuthExtension.GetCurrentUserId(HttpContext);

			var result = await _hairdresserAppService.CreateAsync(createHairdresserDto);
			return Ok(result);
		}

		[HttpGet, Route("GetAllHairdressers")]
		public async Task<IActionResult> GetAllHairdressers()
		{
			var result = await _hairdresserAppService.GetAllHairdressers();

			return Ok(result);
		}

		[HttpGet,Route("GetHairdresser/{guid:id}")]
		public async Task<IActionResult> GetHairdresser(Guid id)
		{
			var result=await _hairdresserAppService.GetHairdresserById(id);

			return Ok(result);
		}

		[HttpPost, Route("GetAllHairdressersByUserAddress")]
		public async Task<IActionResult> GetAllHairdressersByUserAddress([FromBody] PageSearchArgs pageSearchArgs)
		{
			var userId = (Guid)GrpcAuthExtension.GetCurrentUserId(HttpContext);

			var address = await _userGrpcService.GetSelectedUserAddressById(userId);

			var filter = new FilteringOption
			{
				Field = "location",
				Operator = FilteringOperator.Contains,
				Value = address.City + " " + address.State
			};

			pageSearchArgs.FilteringOptions.RemoveAll(x => x.Field == "location");

			pageSearchArgs.FilteringOptions.Add(filter);

			var result = await _hairdresserAppService.GetAllHairdresserByFilter(pageSearchArgs);

			return Ok(result);

		}

	}
}
