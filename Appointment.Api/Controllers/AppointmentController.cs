using Appointment.Application.Dto;
using Appointment.Application.Interfaces.Appointment;
using Grpc.Auth;
using Grpc.Hairdresser.ClientServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace Appointment.Api.Controllers
{
	[Route("/[controller]")]
	[ApiController]
	public class AppointmentController : ControllerBase
	{
		private readonly IAppointmentAppService _appointmentAppService;
		private readonly HairdresserGrpcService _hairdresserGrpcService;

		public AppointmentController(IAppointmentAppService appointmentAppService, HairdresserGrpcService hairdresserGrpcService)
		{
			_appointmentAppService = appointmentAppService;
			_hairdresserGrpcService = hairdresserGrpcService;
		}

		[HttpPost, Route("AddAppointment")]
		public async Task<IActionResult> AddAppointment([FromBody] CreateAppointmentDto createAppointmentDto)
		{
			createAppointmentDto.UserId = (Guid)GrpcAuthExtension.GetCurrentUserId(HttpContext);

			var result = await _appointmentAppService.CreateAppointment(createAppointmentDto);

			//ayrıyetten uygun randevular için method yaz *sonra 

			if (result.IsValid)
				return Ok(result);
			else
				return BadRequest(result);
		}

		[HttpPost, Route("ApprovedAppointment")]
		public async Task<IActionResult> ApprovedAppointment([FromBody] AppointmentStateHairdresserDto appointmentStateHairdresserDto)
		{

			var userId = (Guid)GrpcAuthExtension.GetCurrentUserId(HttpContext);
			var appointment = await _appointmentAppService.GetAppointmentById(appointmentStateHairdresserDto.Id);

			if (appointment == null)
				return BadRequest("Appointment Not Found");

			var hairdresserCheck = await _hairdresserGrpcService.CheckHairdresserIdAndUserId(appointment.HairdresserId, userId);

			if (hairdresserCheck == false)
				return BadRequest("Hairdresser Not Found");

			appointmentStateHairdresserDto.HairdresserId = appointment.HairdresserId;

			var result = await _appointmentAppService.ApproveAppointment(appointmentStateHairdresserDto);

			if (result.IsValid)
				return Ok(result);
			else
				return BadRequest(result);

		}
	}
}
