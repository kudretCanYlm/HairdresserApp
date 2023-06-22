using Appointment.Application.Dto;
using Appointment.Application.Interfaces.Appointment;
using Grpc.Auth;
using Grpc.Hairdresser.ClientServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
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

		//set
		//current date	   29 july 18:40	
		//appointment date 29 july 16:40
		//not agree appointment
		[HttpPost, Route("AddAppointment")]
		public async Task<IActionResult> AddAppointment([FromBody] CreateAppointmentDto createAppointmentDto)
		{
			createAppointmentDto.UserId = (Guid)GrpcAuthExtension.GetCurrentUserId(HttpContext);

			var result = await _appointmentAppService.CreateAppointment(createAppointmentDto);

			if (result.IsValid)
				return Ok(result);
			else
				return BadRequest(result);
		}


		[HttpGet, Route("GetAppointmentsForHairdresser/{hairdresserId:guid}")]
		public async Task<IActionResult> GetAppointmentsForHairdresser(Guid hairdresserId)
		{
			//get userId
			var userId = (Guid)GrpcAuthExtension.GetCurrentUserId(HttpContext);

			//get hairdresser by userId
			var hairdresser = await _hairdresserGrpcService.CheckHairdresserIdAndUserId(hairdresserId, userId);

			if (hairdresser != true)
				return NotFound("Hairdresser Not Found");

			//get appointments by hairdresserId
			var appointments = await _appointmentAppService.GetAppointmentsByHairdresserId(hairdresserId);

			return Ok(appointments);

		}

		//test
		[HttpGet, Route("GetCreatedAppointmentsForHairdresser/{hairdresserId:guid}")]
		public async Task<IActionResult> GetCreatedAppointmentsForHairdresser(Guid hairdresserId)
		{
			//get userId
			var userId = (Guid)GrpcAuthExtension.GetCurrentUserId(HttpContext);

			//get hairdresser by userId
			var hairdresser = await _hairdresserGrpcService.CheckHairdresserIdAndUserId(hairdresserId, userId);

			if (hairdresser != true)
				return NotFound("Hairdresser Not Found");

			//get appointments by hairdresserId
			var appointments = await _appointmentAppService.GetCreatedAppointmentsByHairdresserId(hairdresserId);

			return Ok(appointments);

		}

		//test
		[HttpGet,Route("GetApprovedAppointmentsForHairdresser/{hairdresserId:guid}")]
		public async Task<IActionResult> GetApprovedAppointmentsForHairdresser(Guid hairdresserId)
		{
			//get userId
			var userId = (Guid)GrpcAuthExtension.GetCurrentUserId(HttpContext);

			//get hairdresser by userId
			var hairdresser = await _hairdresserGrpcService.CheckHairdresserIdAndUserId(hairdresserId, userId);

			if (hairdresser != true)
				return NotFound("Hairdresser Not Found");

			//get appointments by hairdresserId
			var appointments = await _appointmentAppService.GetApprovedAppointmentsByHairdresserId(hairdresserId);

			return Ok(appointments);
		}


		[HttpGet,Route("GetUserAppointments")]
		public async Task<IActionResult> GetUserAppointments()
		{
			//get userId
			var userId = (Guid)GrpcAuthExtension.GetCurrentUserId(HttpContext);

			//get appointments by userId
			var appointments=await _appointmentAppService.GetAllAppointmentsByUserId(userId);

			return Ok(appointments);
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

		[HttpPost,Route("GetAllAppointmentsForUser")]
		public async Task<IActionResult> GetAllAppointmentsForUser([FromBody] GetAllAppointmentsForUserPostDto getAllAppointmentsForUserPostDto)
		{	
			var appointments=await _appointmentAppService.GetAllAppointmentsForUser(getAllAppointmentsForUserPostDto);

			return Ok(appointments);
		}
	}
}
