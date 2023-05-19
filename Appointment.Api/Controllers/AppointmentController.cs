using Appointment.Application.Dto;
using Appointment.Application.Interfaces.Appointment;
using Grpc.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.Api.Controllers
{
	[Route("/[controller]")]
	[ApiController]
	public class AppointmentController : ControllerBase
	{
		private readonly IAppointmentAppService _appointmentAppService;

		public AppointmentController(IAppointmentAppService appointmentAppService)
		{
			_appointmentAppService = appointmentAppService;
		}

		[HttpPost,Route("AddAppointment")]
		public async Task<IActionResult> AddAppointment([FromBody] CreateAppointmentDto createAppointmentDto)
		{
			createAppointmentDto.UserId = (Guid)GrpcAuthExtension.GetCurrentUserId(HttpContext);

			var result = await _appointmentAppService.CreateAppointment(createAppointmentDto);

			if (result.IsValid)
				return Ok(result);
			else
				return BadRequest(result);
		}
	}
}
